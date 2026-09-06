using Bib_Hacienda.Clases.Derivados;

namespace Bib_Hacienda.Clases.Factories
{
    // Los controladores dependerán de esta abstracción, no de las clases concretas.
    public abstract class ProductoGanaderoFactory
    {
        public abstract ProductoGanadero CrearProductoGanadero(string id, string nombre, uint precioBase);
    }
}