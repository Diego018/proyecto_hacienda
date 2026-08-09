using System;
using System.Collections.Generic;
using Bib_Hacienda.Clases;
using Bib_Hacienda.Eventos;
using p_mvcHacienda.Infraestructura.puertos;
using p_mvcHacienda.Servicios.Contratos;

namespace p_mvcHacienda.Servicios {

    public class ResService : IResService {

        private readonly IPersistenciaHacienda _haciendaPersistencia;

        public ResService(IPersistenciaHacienda persistencia) {
            _haciendaPersistencia = persistencia;
        }

        public string AlimentarRes(string potreroId, string nombreRes, uint cantidad) {

            try {
                Hacienda hacienda = _haciendaPersistencia.CargarHacienda();
                Potrero potrero = hacienda.buscar_potrero(potreroId);

                if (potrero == null) {
                    throw new InvalidOperationException($"No se encontró el potrero '{potreroId}'");
                }

                Res res = potrero.buscar_res(nombreRes);

                if (res == null) {
                    throw new InvalidOperationException($"No se encontró la res '{nombreRes}' en el potrero '{potreroId}'");
                }

                res.alimentar(cantidad);

                string mensajeEventos = "";

                var publisherPesoMin = new PublisherPesoMin();
                var publisherPesoVenta = new PublisherPesoVenta();

                publisherPesoMin.evt_peso_min += (mensaje) => {
                    if (!string.IsNullOrEmpty(mensaje)) mensajeEventos += mensaje + "\n";
                };

                publisherPesoVenta.evt_peso_venta += (mensaje) => {
                    if (!string.IsNullOrEmpty(mensaje)) mensajeEventos += mensaje + "\n";
                };

                publisherPesoMin.Informar_Peso_Min(res);
                publisherPesoVenta.Informar_Peso_Venta(res);

                _haciendaPersistencia.GuardarHacienda(hacienda);

                string mensajeFinal = $"La res '{res.Nombre}' ha sido alimentada, ahora pesa {res.Peso} kg.";

                if (!string.IsNullOrEmpty(mensajeEventos)) {
                    mensajeFinal += "\n" + mensajeEventos.TrimEnd();
                }

                return mensajeFinal;
            }
            catch (Exception ex) {
                throw new Exception($"Error al alimentar la res: {ex.Message}");
            }
        }

        public Res BuscarRes(string potreroId, string nombreRes) {

            Hacienda hacienda = _haciendaPersistencia.CargarHacienda();
            Potrero potrero = hacienda.buscar_potrero(potreroId);

            return potrero?.buscar_res(nombreRes);
        }

        public List<(Potrero Potrero, Res Res)> ObtenerTodasLasReses() {

            Hacienda hacienda = _haciendaPersistencia.CargarHacienda();
            var resultado = new List<(Potrero, Res)>();

            foreach (var potrero in hacienda.obtener_potreros()) {
                foreach (var res in potrero.obtener_reses()) {
                    resultado.Add((potrero, res));
                }
            }

            return resultado;
        }
    }
}