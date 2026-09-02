using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Infraestructura.puertos;

public interface IPersistenciaProductos {
    
    void GuardarProducto(ProductoGanadero producto);

    List<ProductoGanadero> CargarProductos();
    
}