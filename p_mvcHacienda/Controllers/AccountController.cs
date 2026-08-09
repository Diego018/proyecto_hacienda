using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using p_mvcHacienda.Models;
using p_mvcHacienda.Servicios.contratos;

namespace p_mvcHacienda.Controllers {

    public class AccountController : Controller {

        private readonly IAutenticacionService _autenticacionService;

        public AccountController(IAutenticacionService autenticacionService) {
            _autenticacionService = autenticacionService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null) {

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null) {

            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid) {

                bool esValido = _autenticacionService.ValidarCredenciales(model.Username, model.Password);

                if (esValido) {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.Username) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    if (Url.IsLocalUrl(returnUrl)) {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Usuario o contraseña inválidos.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout() {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied() {
            return View();
        }
    }
}