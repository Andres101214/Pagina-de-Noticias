using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class FabricaLogica
    {
        public static ILogicaNoticia getLogicaNoticia()
        {
            return (LogicaNoticia.GetInstancia());
        }

        public static ILogicaPeriodista getLogicaPeriodista()
        {
            return (LogicaPeriodista.GetInstance());
        }
        
        public static ILogicaSeccion getLogicaSeccion()
        {
            return (LogicaSeccion.Getinstance());
        }
        public static ILogicaUsuario getLogicaUsuario()
        {
            return (LogicaUsuario.Getinstance());
        }
    }
}
