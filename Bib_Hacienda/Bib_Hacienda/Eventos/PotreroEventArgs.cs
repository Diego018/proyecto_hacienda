using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Eventos
{
    public class PotreroEventArgs : EventArgs 
    {
        public Potrero EntidadPotrero { get; }
        public PotreroEventArgs(Potrero potrero) 
        { 
            EntidadPotrero = potrero; 
        }
    }
}