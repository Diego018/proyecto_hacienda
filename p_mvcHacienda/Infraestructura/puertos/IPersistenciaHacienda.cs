using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Infraestructura.puertos;

public interface IPersistenciaHacienda {
    
    Hacienda CargarHacienda();

    void GuardarHacienda(Hacienda hacienda);
    
}