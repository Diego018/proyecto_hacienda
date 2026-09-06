using System;
using Bib_Hacienda.Clases.Derivados;

namespace Bib_Hacienda.Clases.Factories
{
    public class QuesoFactory : LacteoFactory
    {
        private readonly string _tipoEstandar;
        private readonly uint _pesoEstandar;

        public QuesoFactory(string tipoEstandar = "Fresco", uint pesoEstandar = 500)
        {
            _tipoEstandar = tipoEstandar;
            _pesoEstandar = pesoEstandar;
        }

        public override ProductoGanadero CrearProductoGanadero(string id, string nombre, uint precio)
        {
            return new Queso(id, nombre, precio, DateTime.Now, 0.5f, _tipoEstandar, _pesoEstandar);
        }
    }
}