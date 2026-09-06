using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Clases.Factories
{
    public class VacunaFactory
    {
        public Vacuna CrearVacunaViva(string nombre, string lote, DateTime fecha_vencimiento, DateTime fecha_aplicacion, Viva.enum_l_atenuaciones atenuacion)
        {
            return new Viva(nombre, lote, fecha_vencimiento, fecha_aplicacion, atenuacion);
        }

        public Vacuna CrearVacunaBacteriana(string nombre, string lote, DateTime fecha_vencimiento, DateTime fecha_aplicacion, uint periodo_aplicacion)
        {
            return new Bacteriana(nombre, lote, fecha_vencimiento, fecha_aplicacion, periodo_aplicacion);
        }
    }
}