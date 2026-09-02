using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Servicios.contratos;

public interface IProductoService {
    
    string CrearProducto(Res res);

    List<ProductoGanadero> ObtenerProductos();

    ProductoGanadero BuscarProducto(string id);
    
    
}