using System;
using Bib_Hacienda.Clases;
using Bib_Hacienda.Reglas;

namespace Bib_Hacienda.Clases.Validaciones
{
    public class ValidarPotreroHandler : ValidadorHandler
    {
        public override bool Validar(object entidad)
        {
            if (entidad is Potrero potrero)
            {
                int cantidadActual = potrero.obtener_reses().Count;
                if (!ReglaPotrero.validarCapacidad(cantidadActual))
                {
                    throw new InvalidOperationException($"El potrero '{potrero.Identificacion}' alcanzó su capacidad máxima ({ReglaPotrero.max_reses_potrero} reses).");
                }
            }
            return base.Validar(entidad); // Pasa al siguiente eslabón
        }
    }
}