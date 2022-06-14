using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABMSecciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void DesactivoBotones()
    {
        BtnAlta.Enabled = true;
        BtnEliminar.Enabled = false;
        BtnModificar.Enabled = false;
    }

    private void LimpioControles()
    {
        TxtCodigo.Text = "";
        TxtNombre.Text = "";

        TxtCodigo.Enabled = true;
        BtnAlta.Enabled = true;
        BtnEliminar.Enabled = false;
        BtnModificar.Enabled = false;
        LblError.Text = "";
        LblSeccion.Text = "";
        TxtCodigo.ReadOnly = false;
    }
    protected void BtnBuscarSec_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Seccion _unaSec = Logica.FabricaLogica.getLogicaSeccion().BuscarSec(TxtCodigo.Text);
            if (_unaSec == null)
                throw new Exception("La Seccion no existe");
            else
            {
                Session["Seccion"] = _unaSec;
                LblSeccion.Text = _unaSec.NombreSec.ToString();
                TxtNombre.Text = _unaSec.NombreSec.ToString();
                TxtCodigo.Text = _unaSec.CodSec.ToString();
                BtnAlta.Enabled = true;
                BtnEliminar.Enabled = true;
                BtnModificar.Enabled = true;
                TxtCodigo.ReadOnly = true;
                LblError.Text = "";

            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void BtnAlta_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Seccion _unaSec = null;
            _unaSec = new EntidadesCompartidas.Seccion(TxtCodigo.Text, TxtNombre.Text);
            Logica.FabricaLogica.getLogicaSeccion().AltaSec(_unaSec);
            this.DesactivoBotones();
            this.LimpioControles();
            TxtCodigo.Enabled = true;

            LblError.Text = "Alta con éxito";
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void BtnLimpiar_Click(object sender, EventArgs e)
    {
        this.LimpioControles();
        TxtCodigo.Enabled = true;
        TxtCodigo.ReadOnly = false;
        LblError.Text = "";
        LblSeccion.Text = "";
    }

    protected void BtnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Seccion _unaSec = (EntidadesCompartidas.Seccion)Session["Seccion"];
            Logica.FabricaLogica.getLogicaSeccion().EliminarSec(_unaSec);
            this.DesactivoBotones();
            this.LimpioControles();
            TxtCodigo.Enabled = true;
            LblSeccion.Text = "";
            TxtNombre.Text = "";
            TxtCodigo.Text = "";

            LblError.Text = "Baja con éxito";
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void BtnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Seccion _unaSec = (EntidadesCompartidas.Seccion)Session["Seccion"];
            _unaSec.CodSec = TxtCodigo.Text.Trim();
            _unaSec.NombreSec = TxtNombre.Text.Trim();
            Logica.FabricaLogica.getLogicaSeccion().ModificarSec(_unaSec);
            this.DesactivoBotones();
            this.LimpioControles();
            TxtCodigo.Enabled = true;

            LblError.Text = "Modificacion con éxito";
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
}