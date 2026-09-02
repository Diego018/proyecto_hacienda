using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Servicios.contratos;

public interface IVacunaService {
    
    string CrearVacuna(Vacuna vacuna);

    string AplicarVacuna(string potreroId, string nombreRes, Vacuna vacuna);

    List<Vacuna> ObtenerVacunasDisponibles();
    
}