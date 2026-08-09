using Microsoft.AspNetCore.Mvc;
using Bib_Hacienda.Clases;
using p_mvcHacienda.Servicios.Contratos;

namespace p_mvcHacienda.Controllers {

    public class ResController : Controller {

        private readonly IResService _resService;
        private readonly IPotreroService _potreroService;

        public ResController(IResService resService, IPotreroService potreroService) {
            _resService = resService;
            _potreroService = potreroService;
        }

        [HttpGet]
        public ActionResult Index() {

            var resesConPotrero = _resService.ObtenerTodasLasReses();
            return View(resesConPotrero);
        }

        [HttpGet]
        public ActionResult DetalleVacunas(string potreroId, string nombreRes) {

            try {
                Res res = _resService.BuscarRes(potreroId, nombreRes);

                if (res == null) {
                    TempData["Mensaje"] = "Res no encontrada";
                    TempData["TipoMensaje"] = "danger";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.PotreroId = potreroId;
                ViewBag.NombreRes = nombreRes;
                return View(res.VacunasAplicadas);
            }
            catch (Exception ex) {
                TempData["Mensaje"] = ex.Message;
                TempData["TipoMensaje"] = "danger";
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Create() {

            ViewBag.Potreros = _potreroService.ObtenerTodosLosPotreros();
            return View();
        }

        [HttpPost]
        public ActionResult Create(string potreroId, string nombre, DateTime fechaNacimiento, uint peso) {

            try {
                if (string.IsNullOrWhiteSpace(potreroId) || string.IsNullOrWhiteSpace(nombre)) {
                    ViewBag.Mensaje = "Todos los campos son requeridos";
                    ViewBag.TipoMensaje = "danger";
                    ViewBag.Potreros = _potreroService.ObtenerTodosLosPotreros();
                    return View();
                }

                Potrero potrero = _potreroService.ObtenerPotreroPorIdentificacion(potreroId);

                if (potrero == null) {
                    ViewBag.Mensaje = "Potrero no encontrado";
                    ViewBag.TipoMensaje = "danger";
                    ViewBag.Potreros = _potreroService.ObtenerTodosLosPotreros();
                    return View();
                }

                Res res = potrero.Tipo_potrero switch {
                    l_tipos_potreros.Ternero => new Ternero(nombre, peso, fechaNacimiento),
                    l_tipos_potreros.Cebon => new Cebon(nombre, peso, fechaNacimiento),
                    l_tipos_potreros.Novillo => new Novillo(nombre, peso, fechaNacimiento),
                    _ => throw new Exception("Tipo de potrero no reconocido.")
                };

                string mensaje = _potreroService.AgregarRes(potreroId, res);

                TempData["Mensaje"] = mensaje;
                TempData["TipoMensaje"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                ViewBag.Mensaje = ex.Message;
                ViewBag.TipoMensaje = "danger";
            }

            ViewBag.Potreros = _potreroService.ObtenerTodosLosPotreros();
            return View();
        }

        public ActionResult Alimentar(string potreroId, string nombreRes, uint cantidadAlimento) {

            try {
                string mensaje = _resService.AlimentarRes(potreroId, nombreRes, cantidadAlimento);
                TempData["Mensaje"] = mensaje;
                TempData["TipoMensaje"] = "success";
            }
            catch (Exception ex) {
                TempData["Mensaje"] = ex.Message;
                TempData["TipoMensaje"] = "danger";
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Vender(string potreroId, string nombreRes, string monto) {

            try {
                if (!uint.TryParse(monto, out uint montoUint)) {
                    TempData["Mensaje"] = "Monto inválido";
                    TempData["TipoMensaje"] = "danger";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Mensaje"] = "Pendiente: inyectar IVentaService en ResController";
                TempData["TipoMensaje"] = "danger";
            }
            catch (Exception ex) {
                TempData["Mensaje"] = ex.Message;
                TempData["TipoMensaje"] = "danger";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}