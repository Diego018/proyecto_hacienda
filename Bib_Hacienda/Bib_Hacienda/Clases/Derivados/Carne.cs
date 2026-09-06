using System;

namespace Bib_Hacienda.Clases.Derivados
{
    public class Carne : ProductoGanadero
    {
        public uint Peso { get; private set; }
        public string TipoCorte { get; private set; }

        public Carne(string id, string nombre, uint precio, uint peso, string tipoCorte) 
            : base(id, nombre, precio)
        {
            Peso = peso;
            TipoCorte = tipoCorte;
        }

        public override uint CalcularPrecio() => Precio;
    }
}