using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Nacional : Noticia
    {

        private Seccion _Secc;

        public Seccion Secc
        {
            set
            {
                if (value == null)
                    throw new Exception("La noticia nacional debe estár clasificada en una sección.");
                else
                    _Secc = value;
            }
            get { return _Secc; }
        }
        //Constructores
        public Nacional (string NCod, Seccion unaSecc, DateTime NFec, int NImp, string NTitulo, string NCue, List<Periodista> LPer, Usuario unUsuar)
            : base(NCod, NFec, NImp, NTitulo, NCue, LPer, unUsuar)
        {
            Secc = unaSecc;
        }
        



    }
}
