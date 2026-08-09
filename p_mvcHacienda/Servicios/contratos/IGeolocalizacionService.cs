using Bib_Hacienda.Clases;
using Bib_Hacienda.Clases.RefactorBiblioteca;

namespace p_mvcHacienda.Servicios.contratos;

public interface IGeolocalizacionService {
    
    void AsignarChip(Res res, ChipsGeolocalizacion chip);

    void ActualizarPosicion(ChipsGeolocalizacion chip);

    string ObtenerPosicion(Res res);
    
}