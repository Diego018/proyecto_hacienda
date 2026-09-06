using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Interfaces.Estrategias
{
    // Define el contrato común para calcular el precio de cualquier animal
    public interface IEstrategiaCalculoVenta
    {
        uint CalcularMonto(Res animal);
    }
}