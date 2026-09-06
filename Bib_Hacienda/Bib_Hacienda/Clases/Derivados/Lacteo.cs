using System;

namespace Bib_Hacienda.Clases.Derivados
{
    public abstract class Lacteo : ProductoGanadero
    {
        public DateTime FechaProduccion { get; protected set; }
        public float Volumen { get; protected set; }

        protected Lacteo(string id, string nombre, uint precio, DateTime fechaProduccion, float volumen) 
            : base(id, nombre, precio)
        {
            FechaProduccion = fechaProduccion;
            Volumen = volumen;
        }
    }
}