using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;


public partial class AltaModNotNac : System.Web.UI.Page
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

    protected void BtnBuscarNac_Click(object sender, EventArgs e)
    {

        try
        {

            string _CodInt = TxtCodInt.Text;
            Noticia _objeto = Logica.FabricaLogica.getLogicaNoticia().Buscar(_CodInt);
            if (_objeto == null)
            {
                LblError.Text = "No se encontro noticia con este código";
                Session["Nacional"] = _objeto;
                Session["Periodistas"] = new List<Periodista>();
            }
            else if (_objeto is Internacional)
                throw new Exception("El código interno pertenece a una noticia internacional - Error");
            else
            {
                Nacional nac = _objeto as Nacional;
                Session["Periodistas"] = _objeto.ListaPer;
                Session["Nacional"] = _objeto;
                LblNacional.Text = nac.CodInt;
                TxtCuerpoNac.Text = nac.CuerpoN;
                TxtFechaNac.Text = nac.FechaPN.ToString();
                TxtImportanciaNac.Text = nac.Importancia.ToString();
                TxtTituloNac.Text = nac.Titulo;
                TxtSeccionNac.Text = nac.Secc.NombreSec;
                LbUsuMod.Text = _objeto.Usuar.NombreUsu;
                LbPeriodistas.DataSource = Session["Periodistas"];
                LbPeriodistas.DataTextField = "NombrePer";
                LbPeriodistas.DataBind();
                LblError.Text = "Noticia Encontrada";
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
            if (Session["Seccion"] == null)
            {
                throw new Exception("No hay Sección asignada - No se crea la noticia");
            }
            else if (((List<Periodista>)Session["Periodistas"]).Count == 0)
            {
                throw new Exception("No hay Periodista asignado - No se crea la noticia");
            }
            else
            {
                //Doy de alta a la noticia
                Nacional unaNot = new Nacional(TxtCodInt.Text, (Seccion)Session["Seccion"], DateTime.Now, Convert.ToInt32(TxtImportanciaNac.Text), TxtTituloNac.Text, TxtCuerpoNac.Text,(List<Periodista>)Session["Periodistas"], (Usuario)Session["Usuario"]);
                Logica.FabricaLogica.getLogicaNoticia().Alta((Nacional)unaNot);
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
        TxtCuerpoNac.Text = "";
        TxtFechaNac.Text = "";
        TxtImportanciaNac.Text = "";
        TxtSeccionNac.Text = "";
        TxtTituloNac.Text = "";
        LblError.Text = "";
        LblNacional.Text = "";
        Session["Noticia"] = null;
        Session["Periodistas"] = new List<Periodista>();
        LbPeriodistas.DataSource = Session["Periodistas"];
        LbPeriodistas.DataTextField = "NombrePer";
        LbUsuMod.Text = "";
        LbPeriodistas.DataBind();
        LblSeccion.Text = "";
    }

    protected void BtnModificar_Click(object sender, EventArgs e)    {        try        {            Nacional unaNot = (Nacional)Session["Nacional"];            unaNot.ListaPer = (List<Periodista>)Session["Periodistas"];            unaNot.CodInt = TxtCodInt.Text.Trim();            unaNot.FechaPN = Convert.ToDateTime(TxtFechaNac.Text);
            unaNot.Secc.NombreSec = TxtSeccionNac.Text;            unaNot.Importancia = Convert.ToInt32(TxtImportanciaNac.Text);            unaNot.Titulo = TxtTituloNac.Text;            unaNot.CuerpoN = TxtCuerpoNac.Text;            unaNot.Usuar.NombreUsu = LbUsuMod.Text;            Logica.FabricaLogica.getLogicaNoticia().Modificar(unaNot);            this.LimpioControles();
            LblError.Text = "Modificacion con Exito";        }        catch (Exception ex)        {            LblError.Text = ex.Message;        }    }


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



    protected void BuscarSecc_Click(object sender, EventArgs e)
    {
        try
        {
            Seccion _unaSec = Logica.FabricaLogica.getLogicaSeccion().BuscarSec(TxtSeccionNac.Text);
            if (_unaSec == null)
            {
                LblSeccion.Text = "";
                throw new Exception("La Sección no existe");
            }
            else
            {
                Session["Seccion"] = _unaSec;
                LblSeccion.Text = _unaSec.NombreSec.ToString();
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
}
