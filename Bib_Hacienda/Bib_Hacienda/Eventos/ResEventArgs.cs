using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Eventos
{
    public class ResEventArgs : EventArgs 
    {
        public Res EntidadRes { get; }
        public ResEventArgs(Res res) 
        { 
            EntidadRes = res; 
        }
    }
}