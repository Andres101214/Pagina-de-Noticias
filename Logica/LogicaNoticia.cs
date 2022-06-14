using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;
using System.Xml;

namespace Logica
{
    internal class LogicaNoticia : ILogicaNoticia
    {
        //Singleton
        private static LogicaNoticia _instancia = null;
        private LogicaNoticia() { }
        public static LogicaNoticia GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaNoticia();
            return _instancia;
        }

        //Operaciones
        public void Alta(Noticia unaNot)
        {
            if (unaNot is Nacional)
                FabricaPersistencia.getPersistenciaNacional().AltaNac((Nacional)unaNot);
            else
                FabricaPersistencia.getPersistenciaInternacional().AltaInter((Internacional)unaNot);
        }

        public void Modificar(Noticia unaNot)
        {
            if (unaNot is Nacional)
                FabricaPersistencia.getPersistenciaNacional().ModificarNac((Nacional)unaNot);
            else
                FabricaPersistencia.getPersistenciaInternacional().ModificarInter((Internacional)unaNot);
        }
        public Noticia Buscar(string CodInt)
        {
            Noticia _unaNot = null;
            _unaNot = FabricaPersistencia.getPersistenciaNacional().Buscar(CodInt);
            if (_unaNot == null)
                _unaNot = FabricaPersistencia.getPersistenciaInternacional().Buscar(CodInt);
            return _unaNot;
        }
        public List<Noticia> Listar5Dias()
        {
            List<Noticia> _lista = new List<Noticia>();
            _lista.AddRange(FabricaPersistencia.getPersistenciaNacional().ListarNac5Dias());
            _lista.AddRange(FabricaPersistencia.getPersistenciaInternacional().ListarInter5Dias());
            return _lista;
        }
        public List<Noticia> ListarTodas()
        {
            List<Noticia> _lista = new List<Noticia>();
            _lista.AddRange(FabricaPersistencia.getPersistenciaNacional().ListarNac());
            _lista.AddRange(FabricaPersistencia.getPersistenciaInternacional().ListarInter());
            return _lista;
        }

        public XmlDocument ListarNoticias()
        {
            //Obtengo datos
            List<Noticia> _lista = _lista = new List<Noticia>();
            _lista.AddRange(FabricaPersistencia.getPersistenciaNacional().ListarNac());
            _lista.AddRange(FabricaPersistencia.getPersistenciaInternacional().ListarInter());

            //Convierto a xml
            XmlDocument _Documento = new XmlDocument();
            _Documento.LoadXml("<?xml version='1.0' encoding='utf-8' ?> <Raiz> </Raiz>");
            XmlNode _raiz = _Documento.DocumentElement;

            //recorro la lista para crear los nodos
            foreach (Noticia unaN in _lista)
            {
                XmlElement _CodInt = _Documento.CreateElement("Codigo");
                _CodInt.InnerText = unaN.CodInt.ToString();

                XmlElement _FechaPN = _Documento.CreateElement("Fecha");
                _FechaPN.InnerText = unaN.FechaPN.ToString();

                XmlElement _Tipo = _Documento.CreateElement("Tipo");
                _Tipo.InnerText = unaN.GetType().Name.ToString();


                XmlElement _Titulo = _Documento.CreateElement("Titulo");
                _Titulo.InnerText = unaN.Titulo.ToString();

                XmlElement _Importancia = _Documento.CreateElement("Importancia");
                _Importancia.InnerText = unaN.Importancia.ToString();


                XmlElement _Nodo = _Documento.CreateElement("Noticia");
                _Nodo.AppendChild(_CodInt);
                _Nodo.AppendChild(_FechaPN);
                _Nodo.AppendChild(_Tipo);
                _Nodo.AppendChild(_Titulo);
                _Nodo.AppendChild(_Importancia);



                _raiz.AppendChild(_Nodo);
            }
            //Retornar los datos en formato XML
            return _Documento;
        }

    }
}

