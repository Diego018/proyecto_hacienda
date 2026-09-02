using Microsoft.AspNetCore.Mvc;
using Bib_Hacienda.Clases;
using p_mvcHacienda.Servicios.Contratos;
using System.Globalization;
using p_mvcHacienda.Servicios.contratos;

namespace p_mvcHacienda.Controllers {

    public class VacunaController : Controller {

        private readonly IVacunaService _vacunaService;
        private readonly IResService _resService;
        private readonly IPotreroService _potreroService;

        public VacunaController(IVacunaService vacunaService, IResService resService, IPotreroService potreroService) {
            _vacunaService = vacunaService;
            _resService = resService;
            _potreroService = potreroService;
        }

        [HttpGet]
        public ActionResult Index() {

            var vacunas = _vacunaService.ObtenerVacunasDisponibles();
            return View(vacunas);
        }

        [HttpGet]
        public ActionResult Create() {
            return View();
        }

        
        [HttpGet]
        public ActionResult Aplicar() {

            ViewBag.Potreros = _potreroService.ObtenerTodosLosPotreros();
            ViewBag.Reses = _resService.ObtenerTodasLasReses();
            ViewBag.Vacunas = new List<Vacuna>(); // sin catálogo persistido, ver observación pendiente
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string tipoVacuna, string nombre, string lote,
            string fechaVencimiento, string fechaAplicacion,
            uint? periodoAplicacion, Viva.enum_l_atenuaciones? atenuacion) {

            try {
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(lote)) {
                    ViewBag.Mensaje = "El nombre y lote son requeridos";
                    ViewBag.TipoMensaje = "danger";
                    return View();
                }

                if (!DateTime.TryParseExact(fechaVencimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaVenc)) {
                    ViewBag.Mensaje = "Fecha de vencimiento inválida";
                    ViewBag.TipoMensaje = "danger";
                    return View();
                }

                if (!DateTime.TryParseExact(fechaAplicacion, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaAplic)) {
                    ViewBag.Mensaje = "Fecha de aplicación inválida";
                    ViewBag.TipoMensaje = "danger";
                    return View();
                }

                if (fechaAplic > fechaVenc) {
                    ViewBag.Mensaje = "La fecha de aplicación no puede ser posterior a la de vencimiento";
                    ViewBag.TipoMensaje = "danger";
                    return View();
                }

                Vacuna vacuna;

                if (tipoVacuna == "Bacteriana") {
                    if (!periodoAplicacion.HasValue) {
                        ViewBag.Mensaje = "El período de aplicación es requerido para vacunas bacterianas";
                        ViewBag.TipoMensaje = "danger";
                        return View();
                    }
                    vacuna = new Bacteriana(nombre, lote, fechaVenc, fechaAplic, periodoAplicacion.Value);
                }
                else {
                    if (!atenuacion.HasValue) {
                        ViewBag.Mensaje = "La atenuación es requerida para vacunas vivas";
                        ViewBag.TipoMensaje = "danger";
                        return View();
                    }
                    vacuna = new Viva(nombre, lote, fechaVenc, fechaAplic, atenuacion.Value);
                }

                string resultado = _vacunaService.CrearVacuna(vacuna);

                TempData["Mensaje"] = resultado;
                TempData["TipoMensaje"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                ViewBag.Mensaje = $"Error: {ex.Message}";
                ViewBag.TipoMensaje = "danger";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Aplicar(string potreroId, string nombreRes, string tipoVacuna, string nombreVacuna, string lote,
            string fechaVencimiento, string fechaAplicacion, uint? periodoAplicacion, Viva.enum_l_atenuaciones? atenuacion) {

            try {
                if (string.IsNullOrWhiteSpace(potreroId) || string.IsNullOrWhiteSpace(nombreRes)) {
                    ViewBag.Mensaje = "Todos los campos son requeridos";
                    ViewBag.TipoMensaje = "danger";
                    ViewBag.Potreros = _potreroService.ObtenerTodosLosPotreros();
                    return View();
                }

                DateTime.TryParseExact(fechaVencimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaVenc);
                DateTime.TryParseExact(fechaAplicacion, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaAplic);

                Vacuna vacuna = tipoVacuna == "Bacteriana"
                    ? new Bacteriana(nombreVacuna, lote, fechaVenc, fechaAplic, periodoAplicacion ?? 0)
                    : new Viva(nombreVacuna, lote, fechaVenc, fechaAplic, atenuacion ?? Viva.enum_l_atenuaciones.Atenuacion10);

                var resultado = _vacunaService.AplicarVacuna(potreroId, nombreRes, vacuna);

                TempData["Mensaje"] = resultado;
                TempData["TipoMensaje"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                ViewBag.Mensaje = $"Error: {ex.Message}";
                ViewBag.TipoMensaje = "danger";
                ViewBag.Potreros = _potreroService.ObtenerTodosLosPotreros();
                return View();
            }
        }
    }
}