using Microsoft.AspNetCore.Mvc;
using p_mvcHacienda.Servicios.contratos;

namespace p_mvcHacienda.Controllers {

    public class VentaController : Controller {

        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService) {
            _ventaService = ventaService;
        }

        public ActionResult Index() {

            var ventas = _ventaService.ObtenerTodasLasVentas();
            return View(ventas);
        }

        public ActionResult Details(int id) {
            return View();
        }

        public ActionResult Create() {
            return View();
        }

        public ActionResult Edit(int id) {
            return View();
        }

        public ActionResult Delete(int id) {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {

            try {
                string potreroId = collection["potreroId"];
                string nombreRes = collection["nombreRes"];
                uint monto = uint.Parse(collection["monto"]);

                _ventaService.VenderRes(potreroId, nombreRes, monto);

                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        public ActionResult Edit(int id, IFormCollection collection) {

            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        public ActionResult Delete(int id, IFormCollection collection) {

            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}