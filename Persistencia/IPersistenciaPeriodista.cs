using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data;

namespace Persistencia
{
    public interface IPersistenciaPeriodista
    {
        void AltaPer(Periodista unPer);
        void ModificarPer(Periodista unPer);
        void EliminarPer(Periodista unPer);

        Periodista BuscarPer(string CIPer);
        List<Periodista> ListarPer();
        List<Periodista> ListarTodosLosPeriodistas();

    }
}
