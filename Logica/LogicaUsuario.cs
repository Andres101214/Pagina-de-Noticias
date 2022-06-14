using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaUsuario:ILogicaUsuario
    {
        //Singleton
        private static LogicaUsuario _instancia = null;
        private LogicaUsuario() { }
        public static LogicaUsuario Getinstance()
        {
            if (_instancia == null)
                _instancia = new LogicaUsuario();

            return _instancia;
        }
        public Usuario Logueo(string uNom, string uPass)
        {
            IPersistenciaUsuario FUsu = FabricaPersistencia.getPersistenciaUsuario();
            return (FUsu.Logueo(uNom, uPass));
        }
        public Usuario BuscarUsuario(string NomUsu)
        {
            IPersistenciaUsuario FUsu = FabricaPersistencia.getPersistenciaUsuario();
            return (FUsu.BuscarUsuario(NomUsu));
        }
        public void Alta(Usuario unUsuario)
        {
            IPersistenciaUsuario FUsu = FabricaPersistencia.getPersistenciaUsuario();
            FUsu.Alta(unUsuario);
        }
        //public void BajaUsu(Usuario unUsu)
        //{
        //    IPersistenciaUsuario FUsu = FabricaPersistencia.getPersistenciaUsuario();
        //    FUsu.BajaUsu(unUsu);
        //}

        //public void ModificarUsu(Usuario unUsu)
        //{
        //    IPersistenciaUsuario FUsu = FabricaPersistencia.getPersistenciaUsuario();
        //    FUsu.ModificarUsu(unUsu);
        //}


    }

}
