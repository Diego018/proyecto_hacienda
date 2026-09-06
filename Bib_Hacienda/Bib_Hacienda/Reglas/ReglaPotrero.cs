using System;
using Bib_Hacienda.Eventos;

namespace Bib_Hacienda.Reglas {

    public abstract class ReglaPotrero {

        public static readonly ushort max_reses_potrero = 150;

        public static bool validarCapacidad(int cantidadActual) {

            return cantidadActual < max_reses_potrero;
        }

        public static void OnPotreroLleno(object sender, PotreroEventArgs e)
        {

            Console.WriteLine($"\n[ALERTA OBSERVER] -> El potrero '{e.EntidadPotrero.Identificacion}' ha alcanzado su capacidad máxima de {max_reses_potrero} reses\n");
        }

        public static void OnPotreroMitad(object sender, PotreroEventArgs e)
        {
            Console.WriteLine($"\n[ALERTA OBSERVER] -> El potrero '{e.EntidadPotrero.Identificacion}' está al 50% de su capacidad.\n");
        }
    }
}