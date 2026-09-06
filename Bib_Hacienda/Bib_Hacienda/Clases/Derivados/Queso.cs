using System;

namespace Bib_Hacienda.Clases.Derivados
{
    public class Queso : Lacteo
    {
        public string Tipo { get; private set; }
        public uint Peso { get; private set; } 

        public Queso(string id, string nombre, uint precio, DateTime fechaProduccion, float volumen, string tipo, uint peso) 
            : base(id, nombre, precio, fechaProduccion, volumen)
        {
            Tipo = tipo;
            Peso = peso;
        }

        public override uint CalcularPrecio()
        {
            return Precio; 
        }
    }
}