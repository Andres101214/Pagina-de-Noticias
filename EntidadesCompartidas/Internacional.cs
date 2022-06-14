using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Internacional : Noticia
    {
        //Atributo
        private string _Pais;

        //Propiedades
        public string Pais
        {
            get { return _Pais; }
            set
            {
                if ((value.Trim().Length <= 0) || (value.Trim().Length > 20))
                    throw new Exception("El pais debe tener 20 caracteres máximo");
                else
                    _Pais = value;
            }
        }

        //Constructores
        public Internacional(string NCod, string IPais, DateTime NFec, int NImp, string NTitulo, string NCue, List<Periodista> LPer, Usuario unUsuar)
            : base(NCod, NFec, NImp, NTitulo, NCue, LPer, unUsuar)
        {
            Pais = IPais;
        }
    }
}
