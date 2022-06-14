using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public abstract class Noticia
    {
        // Atributos
        private string _CodInt;
        private string _Titulo;
        private DateTime _FechaPN;
        private string _CuerpoN;
        private int _Importancia;

        //Atributos de asociación
        private Usuario _Usuar;
        private List<Periodista> _ListaPer;

        //Propiedades
        public string CodInt
        {
            get { return _CodInt; }
            set
            {
                if ((value.Trim().Length <= 0) || (value.Trim().Length > 20))
                    throw new Exception("El código debe tener un máximo de 20 caracteres");
                else
                    _CodInt = value;
            }
        }

        public string Titulo
        {
            get { return _Titulo; }
            set
            {
                if ((value.Trim().Length <= 0) || (value.Trim().Length > 50))
                    throw new Exception("El título tiene un máximo de 50 caracteres");
                else
                    _Titulo = value;
            }
        }


        public DateTime FechaPN
        {
            set
            {
                if (DateTime.Now >= value)
                    _FechaPN = value;
                else
                    throw new Exception("Fecha inválida.");
            }
            get { return _FechaPN; }
        }

        public string CuerpoN
        {
            get { return _CuerpoN; }
            set
            {
                if ((value.Trim().Length <= 0) || (value.Trim().Length > 8000))
                    throw new Exception("El cuerpo no puede tener más de 8000 caracteres");
                else
                    _CuerpoN = value;
            }
        }

        public int Importancia
        {
            get { return _Importancia; }
            set
            {
                if ((value <= 0) || (value > 5))
                    throw new Exception("La importancia de una noticia debe ser entre 1 a 5");
                else
                    _Importancia = value;
            }
        }

        public Usuario Usuar
        {
            set
            {
                if (value == null)
                    throw new Exception("Las noticias deben estar referenciadas a un usuario.");
                else
                    _Usuar = value;
            }
            get { return _Usuar; }
        }

        public List<Periodista> ListaPer
        {
            set
            {
                if (value == null)
                    throw new Exception("Las noticias deben estar referenciadas a uno o más periodistas.");
                else
                    _ListaPer = value;
            }
            get { return _ListaPer; }
        }

        //Constructores
        public Noticia(string NCod, DateTime NFec, int NImp, string NTitulo, string NCue, List<Periodista> LPer, Usuario unUsuar)
        {
            CodInt = NCod;
            Titulo = NTitulo;
            FechaPN = NFec;
            CuerpoN = NCue;
            Importancia = NImp;
            Usuar = unUsuar;
            ListaPer = LPer;
        }
    }
}