using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Clases.Validaciones
{
    public class ValidarResHandler : ValidadorHandler
    {
        public override bool Validar(object entidad)
        {
            if (entidad is Res res)
            {
                // Delega en ReglaRes indirectamente a través del método de la entidad
                if (!res.ValidarCrecimiento())
                {
                    throw new Exception($"La res '{res.Nombre}' no cumple las condiciones de peso/edad para su categoría.");
                }
            }
            return base.Validar(entidad);
        }
    }
}