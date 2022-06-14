using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABMPeriodistas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void DesactivoBotones()
    {
        BtnAltaPer.Enabled = false;
        BtnEliminarPer.Enabled = false;
        BtnModificarPer.Enabled = false;
    }

    private void ActivoBotones()
    {
        BtnAltaPer.Enabled = true;
        BtnEliminarPer.Enabled = false;
        BtnModificarPer.Enabled = false;
    }
    private void LimpioControles()
    {
        TxtCedula.Text = "";
        TxtNombrePer.Text = "";

        TxtCedula.Enabled = false;
    }
    protected void BtnBuscarPer_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Periodista _unPer = Logica.FabricaLogica.getLogicaPeriodista().BuscarPer(TxtCedula.Text);
            if (_unPer == null)
            {
                this.ActivoBotones();
                throw new Exception("El periodista no existe");
            }

            else
            {
                Session["Periodista"] = _unPer;
                LblPeriodista.Text = _unPer.NombrePer.ToString();
                TxtMail.Text = _unPer.Mail.ToString();
                TxtNombrePer.Text = _unPer.NombrePer.ToString();
                BtnEliminarPer.Enabled = true;
                BtnModificarPer.Enabled = true;
                BtnAltaPer.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void BtnAltaPer_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Periodista _unPer = null;
            _unPer = new EntidadesCompartidas.Periodista(TxtCedula.Text, TxtNombrePer.Text, TxtMail.Text);
            Logica.FabricaLogica.getLogicaPeriodista().AltaPer(_unPer);
            this.DesactivoBotones();
            this.LimpioControles();
            TxtCedula.Enabled = true;
            TxtMail.Text = "";

            LblError.Text = "Alta con éxito";
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void BtnLimpiarPer_Click(object sender, EventArgs e)
    {
        Session["Periodista"] = null;
        this.DesactivoBotones();
        this.LimpioControles();
        TxtCedula.Enabled = true;
        TxtCedula.ReadOnly = false;
        LblPeriodista.Text = "";
        TxtMail.Text = "";
        LblError.Text = "";
    }

    protected void BtnEliminarPer_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Periodista _unPer = (EntidadesCompartidas.Periodista)Session["Periodista"];
            Logica.FabricaLogica.getLogicaPeriodista().EliminarPer(_unPer);
            this.DesactivoBotones();
            this.LimpioControles();
            TxtMail.Text = "";
            LblPeriodista.Text = "";
            TxtCedula.Enabled = true;

            LblError.Text = "Baja con éxito";
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void BtnModificarPer_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Periodista _unPer = (EntidadesCompartidas.Periodista)Session["Periodista"];
            _unPer.CI = TxtCedula.Text.Trim();
            _unPer.NombrePer = TxtNombrePer.Text.Trim();
            _unPer.Mail = TxtMail.Text.Trim();
            Logica.FabricaLogica.getLogicaPeriodista().ModificarPer(_unPer);
            this.DesactivoBotones();
            this.LimpioControles();
            TxtCedula.Enabled = true;
            TxtCedula.ReadOnly = false;
            TxtMail.Text = "";
            LblPeriodista.Text = "";

            LblError.Text = "Modificacion con éxito";
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
}