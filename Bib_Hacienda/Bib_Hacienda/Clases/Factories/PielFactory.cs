using System;
using Bib_Hacienda.Clases.Derivados;

namespace Bib_Hacienda.Clases.Factories
{
    public class PielFactory : ProductoGanaderoFactory
    {
        private readonly float _areaEstandar;
        private readonly string _calidadEstandar;

        public PielFactory(float areaEstandar = 2.5f, string calidadEstandar = "Primera")
        {
            _areaEstandar = areaEstandar;
            _calidadEstandar = calidadEstandar;
        }

        public override ProductoGanadero CrearProductoGanadero(string id, string nombre, uint precio)
        {
            return new Piel(id, nombre, precio, _areaEstandar, _calidadEstandar);
        }
    }
}