using System;

namespace Bib_Hacienda.Clases.Derivados
{
    public abstract class ProductoGanadero
    {
        public string Id { get; protected set; }
        public string Nombre { get; protected set; }
        public uint Precio { get; protected set; }

        protected ProductoGanadero(string id, string nombre, uint precio)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
        }

        public abstract uint CalcularPrecio();
    }
}