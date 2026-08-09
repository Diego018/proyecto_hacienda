using System;
using System.Collections.Generic;
using System.Linq;
using Bib_Hacienda.Clases;
using Bib_Hacienda.Eventos;
using Bib_Hacienda.Reglas;
using p_mvcHacienda.Infraestructura.puertos;
using p_mvcHacienda.Servicios.Contratos;

namespace p_mvcHacienda.Servicios {

    public class PotreroService : IPotreroService {

        private readonly IPersistenciaHacienda _haciendaPersistencia;

        public PotreroService(IPersistenciaHacienda persistencia) {
            _haciendaPersistencia = persistencia;
        }

        public string CrearPotrero(string identificacion, l_tipos_potreros tipo) {

            try {
                Hacienda hacienda = _haciendaPersistencia.CargarHacienda();

                if (hacienda.obtener_potreros().Any(p => p.Identificacion == identificacion)) {
                    throw new InvalidOperationException($"Ya existe un potrero con la identificación '{identificacion}'");
                }

                string resultado = hacienda.crear_potrero(identificacion, tipo);
                _haciendaPersistencia.GuardarHacienda(hacienda);

                return resultado;
            }
            catch (Exception ex) {
                throw new Exception($"Error al crear el potrero: {ex.Message}");
            }
        }

        public List<Potrero> ObtenerTodosLosPotreros() {

            Hacienda hacienda = _haciendaPersistencia.CargarHacienda();
            return hacienda.obtener_potreros().OrderBy(p => p.Identificacion).ToList();
        }

        public Potrero ObtenerPotreroPorIdentificacion(string id) {

            Hacienda hacienda = _haciendaPersistencia.CargarHacienda();
            return hacienda.buscar_potrero(id);
        }

        public string AgregarRes(string potreroId, Res res) {

            try {
                if (res == null || string.IsNullOrWhiteSpace(res.Nombre)) {
                    throw new ArgumentException("El nombre de la res no puede estar vacío.");
                }

                Hacienda hacienda = _haciendaPersistencia.CargarHacienda();
                Potrero potrero = hacienda.buscar_potrero(potreroId);

                if (potrero == null) {
                    throw new InvalidOperationException($"No se encontró el potrero '{potreroId}'");
                }

                if (potrero.buscar_res(res.Nombre) != null) {
                    throw new InvalidOperationException($"Ya existe una res con el nombre '{res.Nombre}' en el potrero '{potreroId}'");
                }

                int cantidadActual = potrero.obtener_reses().Count;

                if (!ReglaPotrero.validarCapacidad(cantidadActual)) {
                    throw new InvalidOperationException($"El potrero '{potreroId}' alcanzó su capacidad máxima ({ReglaPotrero.max_reses_potrero} reses).");
                }

                if (!res.ValidarCrecimiento()) {
                    throw new InvalidOperationException($"La res '{res.Nombre}' no cumple las condiciones de peso/edad para su categoría.");
                }

                string resultado = potrero.anadir_res(res);

                ushort cantidadNueva = (ushort)potrero.obtener_reses().Count;
                string mensajeEventos = "";

                var publisherLleno = new PublisherPotreroLleno();
                var publisherMitad = new PublisherPotreroMitad();

                publisherLleno.evt_potrero_lleno += (mensaje) => mensajeEventos += "\n" + mensaje;
                publisherMitad.evt_potrero_mitad += (mensaje) => mensajeEventos += "\n" + mensaje;

                publisherLleno.Informar_Potrero_Lleno(cantidadNueva, potrero);
                publisherMitad.Informar_Potrero_Mitad(cantidadNueva, potrero);

                _haciendaPersistencia.GuardarHacienda(hacienda);

                return resultado + mensajeEventos;
            }
            catch (Exception ex) {
                throw new Exception($"Error al agregar la res: {ex.Message}");
            }
        }
    }
}