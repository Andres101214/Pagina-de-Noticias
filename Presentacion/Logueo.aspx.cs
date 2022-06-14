using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using EntidadesCompartidas;


public partial class Logueo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Usuario"] != null)
        {
            Usuario unUsu = (Usuario)Session["Usuario"];
            unUsu.NombreUsu.ToString();
        }
        
    
    }
    protected void Volver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }


    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
            string _NomUsu = Login1.UserName.Trim();
            string _Pass = Login1.Password.Trim();
            Usuario _unUsu = Logica.FabricaLogica.getLogicaUsuario().Logueo(_NomUsu, _Pass);

            if (_unUsu == null)
                LblError.Text = "Usuario o Pass Invalidos";
            else
            {
                Session["Usuario"] = _unUsu;
                Response.Redirect("~/PaginaInicialUsu.aspx");
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
}






