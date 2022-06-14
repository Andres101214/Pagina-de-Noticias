using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using Persistencia;

namespace Logica
{
    internal class LogicaPeriodista:ILogicaPeriodista
    {
        //Singleton
        private static LogicaPeriodista _instancia = null;
        private LogicaPeriodista() { }
        public static LogicaPeriodista GetInstance()
        {
            if (_instancia == null)
                _instancia = new LogicaPeriodista();
            return _instancia;
        }
        public void AltaPer(Periodista unPer)
        {
            IPersistenciaPeriodista FPer = FabricaPersistencia.getPersistenciaPeriodista();
            FPer.AltaPer(unPer);
        }

        public void EliminarPer(Periodista unPer)
        {
            IPersistenciaPeriodista FPer = FabricaPersistencia.getPersistenciaPeriodista();
            FPer.EliminarPer(unPer);
        }


        public void ModificarPer(Periodista unPer)
        {
            IPersistenciaPeriodista FPer = FabricaPersistencia.getPersistenciaPeriodista();
            FPer.ModificarPer(unPer);
        }

        public List<Periodista> ListarPer()
        {
            IPersistenciaPeriodista FPer = FabricaPersistencia.getPersistenciaPeriodista();
            return (FPer.ListarPer());
        }

        public List<Periodista> ListarTodosLosPeriodistas()
        {
            IPersistenciaPeriodista FPer = FabricaPersistencia.getPersistenciaPeriodista();
            return (FPer.ListarTodosLosPeriodistas());
        }

        public Periodista BuscarPer(string CIPer)
        {
            IPersistenciaPeriodista FPer = FabricaPersistencia.getPersistenciaPeriodista();
            return (FPer.BuscarPer(CIPer));
        }
    }
}
