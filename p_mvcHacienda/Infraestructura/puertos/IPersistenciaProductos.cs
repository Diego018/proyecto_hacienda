using Bib_Hacienda.Clases;
using Bib_Hacienda.Clases.Derivados;

namespace p_mvcHacienda.Infraestructura.puertos;

public interface IPersistenciaProductos {
    
    void GuardarProducto(ProductoGanadero producto);

    List<ProductoGanadero> CargarProductos();
    
}