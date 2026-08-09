using Bib_Hacienda.Reglas;
using System;

namespace Bib_Hacienda.Clases
{
    public class Cebon : Res {

        public Cebon(string nombre, uint peso, DateTime fechaNacimiento)
            : base(nombre, peso, fechaNacimiento) {}

        public override bool ValidarCrecimiento() {

            return Peso >= ReglaRes.peso_min_cebon
                   && Edad() > ReglaRes.edad_max_ternero
                   && Edad() <= ReglaRes.edad_max_cebon;
        }

        public override bool EstaEnPesoMinimo() {

            return Peso < ReglaRes.peso_min_cebon;
        }

        public override bool EstaAptaParaVenta() {

            return Peso >= ReglaRes.peso_recom_venta_cebon;
        }
    }
}