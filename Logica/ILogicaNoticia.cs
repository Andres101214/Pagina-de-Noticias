using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Xml;

namespace Logica
{
    public interface ILogicaNoticia
    {
        void Alta(Noticia unaNot);
        Noticia Buscar(string CodInt);
        //Noticia BuscarInt(string CodInt);
        void Modificar(Noticia unaNot);
        //List<Internacional> ListarInter();
        //List<Nacional> ListarNac();
        List<Noticia> ListarTodas();
        XmlDocument ListarNoticias();
        List<Noticia> Listar5Dias();
    }
}
