using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public class FabricaPersistencia
    {
        public static IPersistenciaNacional getPersistenciaNacional()
        {
            return (PersistenciaNacional.GetInstancia());
        }

        public static IPersistenciaInternacional getPersistenciaInternacional()
        {
            return (PersistenciaInternacional.GetInstancia());
        }

        public static IPersistenciaPeriodista getPersistenciaPeriodista()
        {
            return (PersistenciaPeriodista.GetInstance());
        }

        public static IPersistenciaSeccion getPersistenciaSeccion()
        {
            return (PersistenciaSeccion.Getinstance());
        }
        public static IPersistenciaUsuario getPersistenciaUsuario()
        {
            return (PersistenciaUsuario.Getinstance());
        }
    }
}
