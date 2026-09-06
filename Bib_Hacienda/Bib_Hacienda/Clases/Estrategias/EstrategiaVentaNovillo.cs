using System;
using Bib_Hacienda.Interfaces.Estrategias;

namespace Bib_Hacienda.Clases.Estrategias
{
    public class EstrategiaVentaNovillo : IEstrategiaCalculoVenta
    {
        public uint CalcularMonto(Res animal)
        {
            uint precioPorKilo = 5500; 
            return animal.Peso * precioPorKilo;
        }
    }
}