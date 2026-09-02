using Microsoft.AspNetCore.Mvc;
using p_mvcHacienda.Servicios.contratos;

namespace p_mvcHacienda.Controllers {

    public class UsuarioController : Controller {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService) {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult Index() {

            var usuarios = _usuarioService.ObtenerTodosLosUsuarios();
            return View(usuarios);
        }

        [HttpGet]
        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string nombre, string contrasena) {

            try {
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(contrasena)) {
                    ViewBag.Mensaje = "Todos los campos son requeridos";
                    ViewBag.TipoMensaje = "danger";
                    return View();
                }

                var resultado = _usuarioService.CrearUsuario(nombre, contrasena);

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
    }
}