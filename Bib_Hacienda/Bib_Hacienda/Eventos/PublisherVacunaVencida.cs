using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Eventos {

    public class PublisherVacunaVencida {

        public delegate void dele_vacuna_vencida(string mensaje);

        public event dele_vacuna_vencida evt_vacuna_vencida;

        public bool Informar_Vacuna_Vencida(Vacuna vacuna) {

            try {

                if (vacuna == null) {

                    throw new ArgumentNullException(nameof(vacuna), "La vacuna no puede ser null");

                }

                bool esta_vencida = vacuna.Fecha_vencimiento <= DateTime.Now;
                int dias_restantes = (vacuna.Fecha_vencimiento - DateTime.Now).Days;
                bool alerta_vencimiento = !esta_vencida && dias_restantes <= 30;

                string mensaje;

                if (esta_vencida) {

                    mensaje = $"[Evento] La vacuna '{vacuna.Nombre}' del lote '{vacuna.Lote}' está vencida desde " +
                              $"{vacuna.Fecha_vencimiento.ToShortDateString()}";

                }

                else if (alerta_vencimiento) {

                    mensaje = $"[Evento] ⚠ ALERTA: La vacuna '{vacuna.Nombre}' del lote '{vacuna.Lote}' vencerá en " +
                              $"{dias_restantes} días ({vacuna.Fecha_vencimiento.ToShortDateString()})";

                }
                else {

                    mensaje = $"[Evento] La vacuna '{vacuna.Nombre}' del lote '{vacuna.Lote}' es válida (vence el " +
                              $"{vacuna.Fecha_vencimiento.ToShortDateString()})";

                }

                evt_vacuna_vencida?.Invoke(mensaje);

                return esta_vencida;
            }

            catch (Exception er) {

                throw new Exception("[evento] Error inesperado en el metodo Informar_Vacuna_Vencida: " + er.Message);

            }

        }

    }

}