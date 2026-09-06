using System;

namespace Bib_Hacienda.Clases.Derivados
{
    public class Piel : ProductoGanadero
    {
        public float Area { get; private set; }
        public string Calidad { get; private set; }

        public Piel(string id, string nombre, uint precio, float area, string calidad) 
            : base(id, nombre, precio)
        {
            Area = area;
            Calidad = calidad;
        }

        public override uint CalcularPrecio() => Precio;
    }
}