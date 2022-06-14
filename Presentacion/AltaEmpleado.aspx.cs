using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AltaEmpleado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BtnAltaUsu.Enabled = false;
    }
    private void DesactivoBotones()
    {
        BtnAltaUsu.Enabled = false;
        //BtnEliminar.Enabled = false;
        //BtnModificar.Enabled = false;
        
    }

    private void LimpioControles()
    {
        TxtContraseña.Text = "";

        //BtnEliminar.Enabled = false;
        //BtnModificar.Enabled = false;
        TxtUsuario.Enabled = false;
        BtnAltaUsu.Enabled = false;
    }
    protected void BtnBuscarUsu_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Usuario _unUsu = Logica.FabricaLogica.getLogicaUsuario().BuscarUsuario(TxtUsuario.Text);
            if (_unUsu == null)
            {
                LblUsuario.Text = "";
                BtnAltaUsu.Enabled = true;
                throw new Exception("El Usuario no existe");
                
            }
            else
            {
                Session["Usuario"] = _unUsu;
                LblUsuario.Text = _unUsu.NombreUsu.Trim() + " Ya existe.";
                TxtUsuario.Text = "";
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void BtnAltaUsu_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Usuario _unUsu = null;
            _unUsu = new EntidadesCompartidas.Usuario(TxtUsuario.Text, TxtContraseña.Text);
            Logica.FabricaLogica.getLogicaUsuario().Alta(_unUsu);
            this.DesactivoBotones();
            this.LimpioControles();
            TxtUsuario.Enabled = true;
            TxtUsuario.Text = "";
            BtnAltaUsu.Enabled = false;
            LblError.Text = "Alta con éxito";
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void BtnLimpiarUsu_Click(object sender, EventArgs e)
    {
        this.DesactivoBotones();
        this.LimpioControles();
        TxtUsuario.Enabled = true;
        TxtUsuario.ReadOnly = false;
        LblError.Text = "";
        LblUsuario.Text = "";
        TxtUsuario.Text = "";
        TxtContraseña.Text = "";
    }
    //protected void BtnModificarUsu_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        EntidadesCompartidas.Usuario _unUsu = (EntidadesCompartidas.Usuario)Session["Usuario"];
    //        _unUsu.NombreUsu = TxtUsuario.Text.Trim();
    //        _unUsu.Contraseña = TxtContraseña.Text.Trim();
    //        Logica.FabricaLogica.getLogicaUsuario().ModificarUsu(_unUsu);
    //        this.DesactivoBotones();
    //        this.LimpioControles();
    //        TxtUsuario.Enabled = true;

    //        LblError.Text = "Modificacion con éxito";
    //    }
    //    catch (Exception ex)
    //    {
    //        LblError.Text = ex.Message;
    //    }
    //}


    //protected void BtnBajaUsu_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        EntidadesCompartidas.Usuario _unUsu = (EntidadesCompartidas.Usuario)Session["Usuario"];
    //        Logica.FabricaLogica.getLogicaUsuario().BajaUsu(_unUsu);
    //        this.DesactivoBotones();
    //        this.LimpioControles();
    //        TxtUsuario.Enabled = true;
    //        LblUsuario.Text = "";
    //        TxtUsuario.Text = "";
    //        TxtContraseña.Text = "";

    //        LblError.Text = "Baja con éxito";
    //    }
    //    catch (Exception ex)
    //    {
    //        LblError.Text = ex.Message;
    //    }
    //}


}
