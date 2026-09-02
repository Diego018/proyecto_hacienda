using p_mvcHacienda.Infraestructura.Implementaciones;
using p_mvcHacienda.Infraestructura.puertos;
using p_mvcHacienda.Servicios;
using p_mvcHacienda.Servicios.contratos;
using p_mvcHacienda.Servicios.Contratos;

namespace p_mvcHacienda {

    public class Program {

        public static void Main(string[] args) {

            var builder = WebApplication.CreateBuilder(args);

            PruebasCaracterizacion.EjecutarCasosASIS();

            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", options => {
                    options.Cookie.Name = "HaciendaSoft.Auth";
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

            builder.Services.AddHttpContextAccessor();

            string directorioDatos = Path.Combine(builder.Environment.ContentRootPath, "Datos");

            builder.Services.AddSingleton<PersistenciaTxtService>(sp =>
                new PersistenciaTxtService(directorioDatos));

            builder.Services.AddSingleton<IPersistenciaHacienda>(sp =>
                sp.GetRequiredService<PersistenciaTxtService>());

            builder.Services.AddSingleton<IPersistenciaVentas>(sp =>
                sp.GetRequiredService<PersistenciaTxtService>());

            builder.Services.AddSingleton<IPersistenciaUsuarios>(sp =>
                sp.GetRequiredService<PersistenciaTxtService>());

            builder.Services.AddSingleton<IPotreroService, PotreroService>();
            builder.Services.AddSingleton<IResService, ResService>();
            builder.Services.AddSingleton<IVacunaService, VacunaService>();
            builder.Services.AddSingleton<IVentaService, VentaService>();
            builder.Services.AddSingleton<IUsuarioService, UsuarioService>();
            builder.Services.AddSingleton<IAutenticacionService, AutenticacionService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}