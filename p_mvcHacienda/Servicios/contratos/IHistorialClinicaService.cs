using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Servicios.contratos;

public interface IHistorialClinicaService {
    
    void CrearHistoria(Res res);

    void AgregarRegistro(HistoriaClinica historia, RegistroClinico registro);

    HistoriaClinica ObtenerHistoria(Res res);
    
}