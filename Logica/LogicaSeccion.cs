using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using Persistencia;

namespace Logica
{
    internal class LogicaSeccion:ILogicaSeccion
    {
        //Singleton
        private static LogicaSeccion _instancia = null;
        private LogicaSeccion() { }
        public static LogicaSeccion Getinstance()
        {
            if (_instancia == null)
                _instancia = new LogicaSeccion();

            return _instancia;
        }



        //Operaciones
        public void AltaSec(Seccion unaSec)
        {
            IPersistenciaSeccion FSec = FabricaPersistencia.getPersistenciaSeccion();
            FSec.AltaSec(unaSec);
        }

        public void EliminarSec(Seccion unaSec)
        {
            IPersistenciaSeccion FSec = FabricaPersistencia.getPersistenciaSeccion();
            FSec.EliminarSec(unaSec);
        }

        public void ModificarSec(Seccion unaSec)
        {
            IPersistenciaSeccion Fsec = FabricaPersistencia.getPersistenciaSeccion();
            Fsec.ModificarSec(unaSec);
        }

        public List<Seccion> ListarSec()
        {
            IPersistenciaSeccion FSec = FabricaPersistencia.getPersistenciaSeccion();
            return (FSec.ListarSec());
        }

        public Seccion BuscarSec(string CodSec)
        {
            IPersistenciaSeccion FSec = FabricaPersistencia.getPersistenciaSeccion();
            return (FSec.BuscarSec(CodSec));
        }

    

    }
}
