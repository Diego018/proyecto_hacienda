using System.Collections.Generic;
using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Servicios.Contratos {

    public interface IResService {

        string AlimentarRes(string potreroId, string nombreRes, uint cantidad);

        Res BuscarRes(string potreroId, string nombreRes);

        List<(Potrero Potrero, Res Res)> ObtenerTodasLasReses();
    }
}