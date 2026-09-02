using Bib_Hacienda.Clases;
using Bib_Hacienda.Eventos;
using p_mvcHacienda.Infraestructura.puertos;
using p_mvcHacienda.Servicios.contratos;

namespace p_mvcHacienda.Servicios {

    public class VacunaService : IVacunaService {

        private readonly IPersistenciaHacienda _haciendaPersistencia;

        public VacunaService(IPersistenciaHacienda persistencia) {
            _haciendaPersistencia = persistencia;
        }

        public string CrearVacuna(Vacuna vacuna) {

            if (vacuna == null) {
                throw new ArgumentNullException(nameof(vacuna));
            }

            return $"Vacuna '{vacuna.Nombre}' del lote '{vacuna.Lote}' creada exitosamente.";
        }

        public string AplicarVacuna(string potreroId, string nombreRes, Vacuna vacuna) {

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

                var publisherVacunaVencida = new PublisherVacunaVencida();
                string mensajeVacuna = "";

                publisherVacunaVencida.evt_vacuna_vencida += (mensaje) => {
                    mensajeVacuna = mensaje;
                };

                bool vacunaVencida = publisherVacunaVencida.Informar_Vacuna_Vencida(vacuna);

                if (vacunaVencida) {
                    throw new InvalidOperationException(mensajeVacuna);
                }

                res.agregarVacuna(vacuna);

                _haciendaPersistencia.GuardarHacienda(hacienda);

                return $"Vacuna aplicada correctamente a la res {res.Nombre}. {mensajeVacuna}";
            }
            catch (Exception ex) {
                throw new Exception($"Error al aplicar la vacuna: {ex.Message}");
            }
        }

        public List<Vacuna> ObtenerVacunasDisponibles() {

            return new List<Vacuna>();
        }
    }
}