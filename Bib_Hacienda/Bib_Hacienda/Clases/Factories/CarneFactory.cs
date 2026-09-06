using System;
using Bib_Hacienda.Clases.Derivados;

namespace Bib_Hacienda.Clases.Factories
{
    public class CarneFactory : ProductoGanaderoFactory
    {
        private readonly uint _pesoEstandar;
        private readonly string _corteEstandar;

        public CarneFactory(uint pesoEstandar = 1, string corteEstandar = "Lomo")
        {
            _pesoEstandar = pesoEstandar;
            _corteEstandar = corteEstandar;
        }

        public override ProductoGanadero CrearProductoGanadero(string id, string nombre, uint precio)
        {
            return new Carne(id, nombre, precio, _pesoEstandar, _corteEstandar);
        }
    }
}