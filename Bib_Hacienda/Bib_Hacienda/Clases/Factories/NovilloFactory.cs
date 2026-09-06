using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Clases.Factories
{
    public class NovilloFactory : ResFactory
    {
        public override Res CrearRes(string nombre, uint peso, DateTime fechaNacimiento)
        {
            return new Novillo(nombre, peso, fechaNacimiento);
        }
    }
}