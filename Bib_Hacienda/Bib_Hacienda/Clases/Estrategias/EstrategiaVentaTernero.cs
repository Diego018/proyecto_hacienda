using System;
using Bib_Hacienda.Interfaces.Estrategias;

namespace Bib_Hacienda.Clases.Estrategias
{
    public class EstrategiaVentaTernero : IEstrategiaCalculoVenta
    {
        public uint CalcularMonto(Res animal)
        {
            // Si mañana cambia el precio del ternero, solo modificamos esta clase.
            uint precioPorKilo = 4000; 
            return animal.Peso * precioPorKilo;
        }
    }
}