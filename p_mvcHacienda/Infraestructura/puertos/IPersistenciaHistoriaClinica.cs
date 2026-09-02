using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Infraestructura.puertos;

public interface IPersistenciaHistoriaClinica {
    
    void GuardarHistoria(HistoriaClinica historia);

    List<HistoriaClinica> CargarHistorias();
    
}