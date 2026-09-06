using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Clases.Factories
{
    public abstract class ResFactory
    {
        public abstract Res CrearRes(string nombre, uint peso, DateTime fechaNacimiento);
    }
}