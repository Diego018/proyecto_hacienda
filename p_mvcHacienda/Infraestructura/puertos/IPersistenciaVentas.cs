using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Infraestructura.puertos;

public interface IPersistenciaVentas {
    
    List<Venta> CargarVentas();

    void GuardarVenta(Venta venta);
    
}