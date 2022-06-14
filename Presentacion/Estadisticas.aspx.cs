using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml.Linq;
using System.Xml;
using Logica;
using EntidadesCompartidas;

public partial class Estadisticas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //La primera vez que se ingresa a la pagina se obtiene la info en XML
            if (!IsPostBack)
            {
                //Obtengo el xml desde el WS
                XmlNode _Documento = FabricaLogica.getLogicaNoticia().ListarNoticias();

                //Xreo y cargo con los datos el documento q me devolvio el WS- formato para Linq
                XElement _documento = XElement.Parse(_Documento.OuterXml);
                Session["Documento"] = _documento;
                CargoInicial();
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
                try
                {
                    XElement _archivoXML = (XElement)(Session["Documento"]);

                    List<Object> mostrar = (from UnaN in _archivoXML.Elements("Noticia")
                                            where UnaN.Element("Tipo").Value == "Nacional"
                                            select new
                                            {
                                                Titulo = UnaN.Element("Titulo").Value,
                                                Importancia = UnaN.Element("Importancia").Value,
                                                Fecha = UnaN.Element("Fecha").Value,
                                                Codigo = UnaN.Element("Codigo").Value,
                                                Tipo = UnaN.Element("Tipo").Value.ToString()
                                            }).ToList<object>();
                    GridView1.DataSource = mostrar;
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    LblError.Text = ex.Message;
                }
            }
            else if (DDLTipoBusqueda.SelectedIndex == 1)
            {
                try
                {
                    XElement _archivoXML = (XElement)(Session["Documento"]);

                    List<Object> mostrar = (from UnaN in _archivoXML.Elements("Noticia")
                                            where UnaN.Element("Tipo").Value == "Internacional"
                                            select new
                                            {
                                                Titulo = UnaN.Element("Titulo").Value,
                                                Importancia = UnaN.Element("Importancia").Value,
                                                Fecha = UnaN.Element("Fecha").Value,
                                                Codigo = UnaN.Element("Codigo").Value,
                                                Tipo = UnaN.Element("Tipo").Value.ToString()
                                            }).ToList<object>();
                    GridView1.DataSource = mostrar;
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    LblError.Text = ex.Message;
                }
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }

    }

    protected void CargoInicial()
    {
        XElement _archivoXML = (XElement)(Session["Documento"]);

        List<Object> mostrar = (from UnaN in (_archivoXML.Elements("Noticia"))
                                select new
                                {
                                    Titulo = UnaN.Element("Titulo").Value,
                                    Importancia = UnaN.Element("Importancia").Value,
                                    Fecha = UnaN.Element("Fecha").Value,
                                    Codigo = UnaN.Element("Codigo").Value,
                                    Tipo = UnaN.Element("Tipo").Value.ToString()

                                }
                                      ).ToList<object>();

        GridView1.DataSource = mostrar;
        GridView1.DataBind();
    }

    protected void DDLTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void BtnLimpiar_Click(object sender, EventArgs e)
    {
        CargoInicial();
        LblError.Text = "";
    }

    protected void BtnFiltrotipo_Click(object sender, EventArgs e)
    {
    }
}