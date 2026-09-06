using System;
using Bib_Hacienda.Clases.Derivados;

namespace Bib_Hacienda.Clases.Factories
{
    public class LecheFactory : LacteoFactory
    {
        private readonly float _grasaEstandar;

        public LecheFactory(float grasaEstandar = 3.5f)
        {
            _grasaEstandar = grasaEstandar;
        }

        public override ProductoGanadero CrearProductoGanadero(string id, string nombre, uint precio)
        {
            // La fábrica asume la responsabilidad de proveer volumen y fecha, liberando al Controlador.
            return new Leche(id, nombre, precio, DateTime.Now, 1.0f, _grasaEstandar);
        }
    }
}