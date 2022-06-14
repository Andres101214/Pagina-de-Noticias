using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace EntidadesCompartidas
{
    public class Usuario
    {
        //Atributos
        private string _NombreUsu;
        private string _Contraseña;


        //Propiedades
        public string NombreUsu
        {
            get { return _NombreUsu; }
            set
            {
                if (value.Trim().Length != 10)
                    throw new Exception("El nombre de usuario debe tener 10 caracteres.");
                else
                    _NombreUsu = value;
            }
        }

        //public string Contraseña
        //{
        //    get { return _Contraseña; }
        //    set
        //    {
        //        if (value.Trim().Length != 7)
        //            throw new Exception("La contraseña debe tener 4 letras y 3 números.");
        //        else
        //            _Contraseña = value;
        //    }
        //}

        public string Contraseña
        {
            get { return _Contraseña; }
            set
            {
                if (value.Trim().Length != 7)
                    throw new Exception("La contraseña debe tener 7 caracteres alfanuméricos, 4 letras y 3 números.");
                else if (contraseña_bien_escrito(value))
                    _Contraseña = value;
                else
                    throw new Exception("El formato de la contraseña no es correcto");
            }

        }

        private Boolean
        contraseña_bien_escrito(String _Contraseña)
        {
            String expresion2;
            expresion2 = "[A-zA-Z]{4}[0-9]{3}";
            if (Regex.IsMatch(_Contraseña, expresion2))
            {
                if (Regex.Replace(_Contraseña, expresion2, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Constructores
        public Usuario(string UNom, string UPass)
        {
            NombreUsu = UNom;
            Contraseña = UPass;
        }

    }
}
