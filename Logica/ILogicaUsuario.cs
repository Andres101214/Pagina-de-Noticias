using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaUsuario
    {
        Usuario Logueo(string UNom, string UPass);
        Usuario BuscarUsuario(string NomUsu);
        void Alta(Usuario unUsuario);
        //void ModificarUsu(Usuario unUsu);
        //void BajaUsu(Usuario unUsu);
    }
}
