using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Servicios.contratos;

public interface IVentaService {
    
    string VenderRes(string potreroId, string nombreRes, uint monto);

    List<Venta> ObtenerTodasLasVentas();
    
    
}