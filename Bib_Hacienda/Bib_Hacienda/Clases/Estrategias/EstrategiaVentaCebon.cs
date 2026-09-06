using System;
using Bib_Hacienda.Interfaces.Estrategias;

namespace Bib_Hacienda.Clases.Estrategias
{
    public class EstrategiaVentaCebon : IEstrategiaCalculoVenta
    {
        public uint CalcularMonto(Res animal)
        {
            uint precioPorKilo = 5000; 
            return animal.Peso * precioPorKilo;
        }
    }
}