using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    public interface IPersistenciaInternacional
    {
        void AltaInter(Internacional unaInter);
        void ModificarInter(Internacional unaInter);
        Internacional Buscar(string CodInt);
        List<Internacional> ListarInter();
        List<Internacional> ListarInter5Dias();


    }
}
