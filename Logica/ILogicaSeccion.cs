using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data;

namespace Logica
{
    public interface ILogicaSeccion
    {
        void AltaSec(Seccion unaSec);
        void EliminarSec(Seccion unaSec);
        void ModificarSec(Seccion unaSec);
        List<Seccion> ListarSec();
        Seccion BuscarSec(string CodSec);

    }
}
