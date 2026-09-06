using Bib_Hacienda.Clases.Derivados;

namespace Bib_Hacienda.Clases.Factories
{
    // Solo define comportamiento común para fábricas de lácteos, pero sigue cumpliendo el contrato superior.
    public abstract class LacteoFactory : ProductoGanaderoFactory
    {
        // Puede contener lógica común de instanciación para lácteos si el negocio lo requiere.
        // Mantiene la firma obligatoria de la fábrica superior.
        public abstract override ProductoGanadero CrearProductoGanadero(string id, string nombre, uint precio);
    }
}