using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;

public partial class AltaModNotInt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.LimpioControles();
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
            //Verifico que tenga Seccion
            if (((List<Periodista>)Session["Periodistas"]).Count == 0)
            {
                throw new Exception("No hay Periodista asignado - No se crea la noticia");
            }
            else
            {
                //Doy de alta a la noticia
                Internacional unaNot = new Internacional(TxtCodInt.Text, TxtPaisInter.Text, DateTime.Now, Convert.ToInt32(TxtImportanciaInter.Text), TxtTituloInter.Text, TxtCuerpoInter.Text, (List<Periodista>)Session["Periodistas"], (Usuario)Session["Usuario"]);
                Logica.FabricaLogica.getLogicaNoticia().Alta((Internacional)unaNot);
                this.LimpioControles();

                //Si llego aca todo ok
                LblError.Text = "Alta con Exito";

            }

        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
    protected void BtnLimpiar_Click(object sender, EventArgs e)
    {
        this.LimpioControles();
    }

    private void LimpioControles()
    {
        TxtCodInt.Text = "";
        TxtCuerpoInter.Text = "";
        TxtFechaInter.Text = "";
        TxtImportanciaInter.Text = "";
        TxtPaisInter.Text = "";
        TxtTituloInter.Text = "";
        LblError.Text = "";
        LblInternacional.Text = "";
        Session["Noticia"] = null;
        Session["Periodistas"] = new List<Periodista>();
        LbPeriodistas.DataSource = Session["Periodistas"];
        LbPeriodistas.DataTextField = "NombrePer";
        LbPeriodistas.DataBind();
        LbUsuMod.Text = "";
    }
    protected void BtnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            Internacional unaNot = (Internacional)Session["Internacionales"];
            unaNot.ListaPer = (List<Periodista>)Session["Periodistas"];
            unaNot.CodInt = TxtCodInt.Text.Trim();
            unaNot.FechaPN = Convert.ToDateTime(TxtFechaInter.Text);
            unaNot.Pais = TxtPaisInter.Text;
            unaNot.Importancia = Convert.ToInt32(TxtImportanciaInter.Text);
            unaNot.Titulo = TxtTituloInter.Text;
            unaNot.CuerpoN = TxtCuerpoInter.Text;
            unaNot.Usuar.NombreUsu = LbUsuMod.Text;
            Logica.FabricaLogica.getLogicaNoticia().Modificar(unaNot);

            LblError.Text = "Modificacion con Exito";
            this.LimpioControles();
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void LbPeriodistas_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (LbPeriodistas.SelectedIndex > 0)
        {
            LbPeriodistas.Text = LbPeriodistas.SelectedItem.Text;

        }
    }


    protected void AgregarPer_Click(object sender, EventArgs e)
    {
        try
        {
            Periodista P = Logica.FabricaLogica.getLogicaPeriodista().BuscarPer(TxtAgregarPer.Text.Trim());

            if (P != null)
            {
                ((List<Periodista>)Session["Periodistas"]).Add(P);
                LbPeriodistas.DataSource = Session["Periodistas"];
                LbPeriodistas.DataTextField = "NombrePer";
                LbPeriodistas.DataBind();
                TxtAgregarPer.Text = "";
            }
            else
                LblError.Text = "Periodista no existe";
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
    protected void BtnQuitarPer_Click(object sender, EventArgs e)
    {
        ((List<Periodista>)Session["Periodistas"]).RemoveAt(LbPeriodistas.SelectedIndex);
        LbPeriodistas.DataSource = Session["Periodistas"];
        LbPeriodistas.DataTextField = "NombrePer";
        LbPeriodistas.DataBind();
        TxtAgregarPer.Text = "";
    }




    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
        try
        {

            string _CodInt = TxtCodInt.Text;
            Noticia _objeto = Logica.FabricaLogica.getLogicaNoticia().Buscar(_CodInt);
            if (_objeto == null)
            {
                LblError.Text = "El código de noticia no existe";
                Session["Internacionales"] = _objeto;
                Session["Periodistas"] = new List<Periodista>();
            }
            else if (_objeto is Nacional)
                throw new Exception("El código interno pertenece a una noticia nacional - Error");
            else
            {
                Internacional inter = _objeto as Internacional;
                Session["Periodistas"] = _objeto.ListaPer;
                Session["Internacionales"] = _objeto;
                LblInternacional.Text = inter.CodInt;
                TxtCuerpoInter.Text = inter.CuerpoN;
                TxtFechaInter.Text = inter.FechaPN.ToString();
                TxtImportanciaInter.Text = inter.Importancia.ToString();
                TxtTituloInter.Text = inter.Titulo;
                TxtPaisInter.Text = inter.Pais;
                LbUsuMod.Text = _objeto.Usuar.NombreUsu;
                LbPeriodistas.DataSource = Session["Periodistas"];
                LbPeriodistas.DataTextField = "NombrePer";
                LbPeriodistas.DataBind();
                BtnAltaInter.Enabled = true;
                BtnModificarInter.Enabled = true;
                LblError.Text = "Noticia Encontrada";
            }

        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }

    }
}
