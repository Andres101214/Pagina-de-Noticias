using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Seccion
    {
        //Atributo
        private string _CodSec;
        private string _NombreSec;

        //Propiedades
        public string CodSec
        {
            get { return _CodSec; }
            set
            {
                if (value.Trim().Length != 5)
                    throw new Exception("El código de sección debe tener 5 caracteres de largo.");
                else
                    _CodSec = value;
            }
        }

        public string NombreSec
        {
            get { return _NombreSec; }
            set
            {
                if ((value.Trim().Length <= 0) || (value.Trim().Length > 20))
                    throw new Exception("El nombre no puede ser mayor de 20 caracteres de largo.");
                else
                    _NombreSec = value;
            }
        }

        //Constructores
        public Seccion(string sCod, string sNom)
        {
            CodSec = sCod;
            NombreSec = sNom;


        }
    }
}