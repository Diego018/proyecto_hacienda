using Bib_Hacienda.Clases;
using p_mvcHacienda.Infraestructura.puertos;
using p_mvcHacienda.Servicios.contratos;


namespace p_mvcHacienda.Servicios {

    public class VentaService : IVentaService {

        private readonly IPersistenciaVentas _ventaPersistencia;
        private readonly IPersistenciaHacienda _haciendaPersistencia;

        public VentaService(IPersistenciaVentas ventaPersistencia, IPersistenciaHacienda haciendaPersistencia) {
            _ventaPersistencia = ventaPersistencia;
            _haciendaPersistencia = haciendaPersistencia;
        }

        public string VenderRes(string potreroId, string nombreRes, uint monto) {

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

                Usuario usuarioSistema = new Usuario("sistema", "");
                Venta venta = new Venta(usuarioSistema, potrero, DateTime.Now, res, monto);

                potrero.eliminar_res(nombreRes);

                _haciendaPersistencia.GuardarHacienda(hacienda);
                _ventaPersistencia.GuardarVenta(venta);

                return $"Venta de la res '{res.Nombre}' realizada con éxito.";
            }
            catch (Exception ex) {
                throw new Exception($"Error al vender la res: {ex.Message}");
            }
        }

        public List<Venta> ObtenerTodasLasVentas() {

            return _ventaPersistencia.CargarVentas().OrderByDescending(v => v.Fecha).ToList();
        }
    }
}