using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;

public partial class MPUsuario : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Session["Usuario"] is Usuario))
            Response.Redirect("~/Logueo.aspx");
        if (Session["Usuario"] != null)
        {
            Usuario unUsu = (Usuario)Session["Usuario"];
            LblUsu.Text = unUsu.NombreUsu;

        }
    }


    protected void BtnSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}

