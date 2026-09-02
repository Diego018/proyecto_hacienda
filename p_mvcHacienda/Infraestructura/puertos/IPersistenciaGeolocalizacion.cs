using Bib_Hacienda.Clases.RefactorBiblioteca;

namespace p_mvcHacienda.Infraestructura.puertos;

public interface IPersistenciaGeolocalizacion {
    
    void GuardarChip(ChipsGeolocalizacion chip);

    List<ChipsGeolocalizacion> CargarChips();
    
}