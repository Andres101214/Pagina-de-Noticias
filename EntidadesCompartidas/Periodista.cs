using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EntidadesCompartidas
{
    public class Periodista
    {
        //Atributo
        private string _CI;
        private string _Mail;
        private string _NombrePer;

        //Propiedades
        public string CI
        {
            get { return _CI; }
            set
            {
                if (value.Trim().Length != 8)
                    throw new Exception("La cédula debe tener 8 dígitos sin puntos ni guiones");
                else
                    _CI = value;
            }
        }

        public string Mail
        {
            get { return _Mail; }
            set
            {
                if ((value.Trim().Length <= 0) || (value.Trim().Length > 20))
                    throw new Exception("El mail debe tener 20 caracteres máximo");
                else
                    _Mail = value;
            }
        }





        //public string Mail
        //{
        //    get { return _Mail; }
        //    set
        //    {
        //        if ((value.Trim().Length <= 0) || (value.Trim().Length > 20))
        //            throw new Exception("El mail debe tener 20 caracteres máximo");
        //        else if (mail_bien_escrito(value))
        //            _Mail = value;
        //        else
        //            throw new Exception("El mail no tiene el formato correcto");
        //    }
        //}
        //private Boolean
        //mail_bien_escrito(String _Mail)
        //{
        //    String expresion;
        //    expresion = "\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        //    if (Regex.IsMatch(_Mail, expresion))
        //    {
        //        if (Regex.Replace(_Mail, expresion, String.Empty).Length == 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public string NombrePer
        {
            get { return _NombrePer; }
            set
            {
                if ((value.Trim().Length <= 0) || (value.Trim().Length > 20))
                    throw new Exception("El nombre del periodista puede tener 20 caracteres máximo");
                else
                    _NombrePer = value;
            }
        }

        //Constructores
        public Periodista(string pCi, string pNom, string pMail)
        {
            CI = pCi;
            NombrePer = pNom;
            Mail = pMail;

        }
    }
}
