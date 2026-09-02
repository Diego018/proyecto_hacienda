namespace Bib_Hacienda.Reglas {

    public abstract class ReglaPotrero {

        public static readonly ushort max_reses_potrero = 150;

        public static bool validarCapacidad(int cantidadActual) {

            return cantidadActual < max_reses_potrero;
        }
    }
}