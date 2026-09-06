using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Clases.Validaciones
{
    public class ValidarVacunaHandler : ValidadorHandler
    {
        public override bool Validar(object entidad)
        {
            if (entidad is Vacuna vacuna)
            {
                if (DateTime.Now > vacuna.Fecha_vencimiento)
                {
                    throw new Exception($"La vacuna '{vacuna.Nombre}' lote '{vacuna.Lote}' está vencida.");
                }
            }
            return base.Validar(entidad);
        }
    }
}