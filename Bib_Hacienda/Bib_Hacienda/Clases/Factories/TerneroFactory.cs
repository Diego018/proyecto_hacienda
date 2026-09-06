using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Clases.Factories
{
    public class TerneroFactory : ResFactory
    {
        public override Res CrearRes(string nombre, uint peso, DateTime fechaNacimiento)
        {
            return new Ternero(nombre, peso, fechaNacimiento);
        }
    }
}