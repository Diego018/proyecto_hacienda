using System;

namespace Bib_Hacienda.Clases.Derivados
{
    public class Leche : Lacteo
    {
        public float PorcentajeGrasa { get; private set; }

        public Leche(string id, string nombre, uint precio, DateTime fechaProduccion, float volumen, float porcentajeGrasa) 
            : base(id, nombre, precio, fechaProduccion, volumen)
        {
            PorcentajeGrasa = porcentajeGrasa;
        }

        public override uint CalcularPrecio()
        {
            return Precio; 
        }
    }
}