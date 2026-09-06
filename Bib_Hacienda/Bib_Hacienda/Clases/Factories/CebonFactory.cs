using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Clases.Factories
{
    public class CebonFactory : ResFactory
    {
        public override Res CrearRes(string nombre, uint peso, DateTime fechaNacimiento)
        {
            return new Cebon(nombre, peso, fechaNacimiento);
        }
    }
}