using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using EntidadesCompartidas;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //obtengo toda las noticias sin filtrar de la base de datos
                Session["_listaTotal"] = Logica.FabricaLogica.getLogicaNoticia().Listar5Dias();
                CargaInicial();
            }
            catch (Exception ex)
            {
                LblError.Text = ex.Message;
            }
        }
    }



    protected void Ingresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Logueo.aspx");
    }



    protected void DDLTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DDLTipoBusqueda.SelectedIndex == 1)
        {
            TxtFiltro.Enabled = false;
            BtnBuscarSeccion.Enabled = false;
            BtnFiltrar.Enabled = true;
            TxtFiltro.Text = "";
            LblSeccion.Text = "";
        }
    }

    protected void BtnBuscarSeccion_Click(object sender, EventArgs e)
    {
        try
        {
            Seccion _unaSec = Logica.FabricaLogica.getLogicaSeccion().BuscarSec(TxtFiltro.Text);
            if (_unaSec == null)
                throw new Exception("La Seccion no existe");
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
    protected void BtnFiltrar_Click(object sender, EventArgs e)
    {
        try
        {

            if (DDLTipoBusqueda.SelectedIndex == 0)
            {
                //2.- determino la consulta = solo quiero Las noticias nacionales cuya seccion sea igual al TxtFiltro
                var resultado = from unaNot in ((List<Noticia>)Session["_listaTotal"])
                                where unaNot is Nacional && ((Nacional)unaNot).Secc.CodSec == TxtFiltro.Text
                                select unaNot;

                //3.- Despliego el resultado en el listbox

                GVNoticias.DataSource = (from unaN in resultado
                                         select new
                                         {
                                             Fecha = unaN.FechaPN,
                                             Titulo = unaN.Titulo,
                                             Tipo = unaN.GetType().Name

                                         }).ToList();
                GVNoticias.DataBind();
            }
            else if (DDLTipoBusqueda.SelectedIndex == 1)
            {
                var resultado = from unaNot in ((List<Noticia>)Session["_listaTotal"])
                                where unaNot is Internacional
                                select unaNot;

                GVNoticias.DataSource = (from unaInt in resultado
                                         select new
                                         {
                                             Fecha = unaInt.FechaPN,
                                             Titulo = unaInt.Titulo,
                                             Tipo = unaInt.GetType().Name
                                         }).ToList();
                GVNoticias.DataBind();
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void BtnLimpiar_Click(object sender, EventArgs e)
    {
        CargaInicial();
        TxtFiltro.Enabled = true;
        BtnBuscarSeccion.Enabled = true;
        BtnFiltrar.Enabled = true;
        TxtFiltro.Text = "";
        DDLTipoBusqueda.SelectedIndex = 0;
        LblSeccion.Text = "";
        TxtFecha.Text = "";
    }
    protected void CargaInicial()
    {
        GVNoticias.DataSource = (from unaN in ((List<Noticia>)Session["_listaTotal"])
                                 select new
                                 {
                                     Fecha = unaN.FechaPN,
                                     Titulo = unaN.Titulo,
                                     Tipo = unaN.GetType().Name

                                 }).ToList();
        GVNoticias.DataBind();
    }

    protected void BtnFiltroFecha_Click(object sender, EventArgs e)
    {

        try
        {
            Session["ListaFecha"] = Logica.FabricaLogica.getLogicaNoticia().ListarTodas();
            var resultado = from unaNot in ((List<Noticia>)Session["ListaFecha"])
                            where unaNot.FechaPN == Convert.ToDateTime(TxtFecha.Text)
                            select unaNot;

            GVNoticias.DataSource = (from unaInt in resultado
                                     select new
                                     {
                                         Fecha = unaInt.FechaPN,
                                         Titulo = unaInt.Titulo,
                                         Tipo = unaInt.GetType().Name
                                     }).ToList();
            GVNoticias.DataBind();
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void GVNoticias_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Noticia unaNot = Logica.FabricaLogica.getLogicaNoticia().Buscar(GVNoticias.SelectedRow.Cells[0].Text);
            if (unaNot == null)
                throw new Exception("No se encontro la noticia");
            else
            {
                Response.Redirect("ConsultaIndivNot.aspx");
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
}