using System;
using Bib_Hacienda.Clases;
using Bib_Hacienda.Reglas;

namespace Bib_Hacienda.Eventos {

    public class PublisherPotreroLleno {

        public delegate void delegado_potrero_lleno(string mensaje);

        public event delegado_potrero_lleno evt_potrero_lleno;

        public void Informar_Potrero_Lleno(ushort cantidad_reses, Potrero potrero) {

            try {

                if (cantidad_reses == ReglaPotrero.max_reses_potrero) {

                    string mensaje =
                        $"[Evento] El potrero '{potrero.Identificacion}' ha alcanzado su capacidad máxima de reses (" +
                        $"{ReglaPotrero.max_reses_potrero}). No se pueden agregar más reses.";


                    evt_potrero_lleno?.Invoke(mensaje);

                }

            }
            catch (Exception er) {

                throw new Exception("[Evento] Error inesperado en el metodo Informar_Potrero_Lleno: " + er.Message);

            }

        }
    }

}