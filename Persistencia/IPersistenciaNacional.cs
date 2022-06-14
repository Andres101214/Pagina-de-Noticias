using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaNacional
    {
        void AltaNac(Nacional unaNac);
        void ModificarNac(Nacional unaNac);
        Nacional Buscar(string CodInt);
        List<Nacional> ListarNac();
        List<Nacional> ListarNac5Dias();


    }
}
