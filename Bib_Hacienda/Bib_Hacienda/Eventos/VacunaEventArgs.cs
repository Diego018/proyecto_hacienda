using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Eventos
{
    public class VacunaEventArgs : EventArgs 
    {
        public Vacuna EntidadVacuna { get; }
        public VacunaEventArgs(Vacuna vacuna) 
        { 
            EntidadVacuna = vacuna; 
        }
    }
}