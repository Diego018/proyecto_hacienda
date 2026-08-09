using System;
using Bib_Hacienda.Clases;
using Bib_Hacienda.Reglas;

namespace p_mvcHacienda {

    public static class PruebasCaracterizacion {

        public static void EjecutarCasosASIS() {

            Console.WriteLine("=== CASOS DE CARACTERIZACIÓN - SISTEMA REFACTORIZADO (TO-BE) ===\n");

            EjecutarCaso("CC-01", () => {
                Potrero p = new Potrero("P1", l_tipos_potreros.Ternero);
                return AgregarConValidacion(p, new Ternero("Vaca1", 200, DateTime.Now.AddMonths(-10)));
            });

            EjecutarCaso("CC-02", () => {
                Potrero p = new Potrero("P1", l_tipos_potreros.Ternero);
                return AgregarConValidacion(p, new Ternero("Vaca2", 200, DateTime.Now.AddMonths(-20)));
            });

            EjecutarCaso("CC-03", () => {
                Potrero p = new Potrero("P2", l_tipos_potreros.Cebon);
                return AgregarConValidacion(p, new Cebon("Vaca3", 350, DateTime.Now.AddMonths(-30)));
            });

            EjecutarCaso("CC-04", () => {
                Potrero p = new Potrero("P2", l_tipos_potreros.Cebon);
                return AgregarConValidacion(p, new Cebon("Vaca4", 350, DateTime.Now.AddMonths(-5)));
            });

            EjecutarCaso("CC-05", () => {
                Potrero p = new Potrero("P3", l_tipos_potreros.Novillo);
                return AgregarConValidacion(p, new Novillo("Vaca5", 450, DateTime.Now.AddMonths(-55)));
            });

            EjecutarCaso("CC-06", () => {
                Potrero p = new Potrero("P4", l_tipos_potreros.Novillo);
                for (int i = 0; i < 150; i++) {
                    AgregarConValidacion(p, new Novillo($"Res{i}", 450, DateTime.Now.AddMonths(-55)));
                }
                return AgregarConValidacion(p, new Novillo("VacaExtra", 450, DateTime.Now.AddMonths(-55)));
            });

            EjecutarCaso("CC-07", () => {
                Potrero p = new Potrero("P1", l_tipos_potreros.Ternero);
                return AgregarConValidacion(p, new Ternero("", 200, DateTime.Now.AddMonths(-10)));
            });

            EjecutarCaso("CC-08", () => {
                Potrero p = new Potrero("P1", l_tipos_potreros.Ternero);
                AgregarConValidacion(p, new Ternero("Vaca1", 200, DateTime.Now.AddMonths(-10)));
                Res encontrada = p.buscar_res("Vaca1");
                return $"Res encontrada: {encontrada.Nombre}, Edad: {encontrada.Edad()}, Peso: {encontrada.Peso}";
            });
        }

        // Replica las validaciones que aplica PotreroService.AgregarRes,
        // ya que Potrero.anadir_res por sí solo no valida nada (SRP).
        static string AgregarConValidacion(Potrero potrero, Res res) {

            if (res == null || string.IsNullOrWhiteSpace(res.Nombre)) {
                throw new ArgumentException("El nombre de la res no puede estar vacío.");
            }

            if (potrero.buscar_res(res.Nombre) != null) {
                throw new InvalidOperationException($"Ya existe una res con el nombre '{res.Nombre}' en el potrero '{potrero.Identificacion}'");
            }

            int cantidadActual = potrero.obtener_reses().Count;

            if (!ReglaPotrero.validarCapacidad(cantidadActual)) {
                throw new InvalidOperationException($"El potrero '{potrero.Identificacion}' alcanzó su capacidad máxima ({ReglaPotrero.max_reses_potrero} reses).");
            }

            if (!res.ValidarCrecimiento()) {
                throw new Exception($"La res '{res.Nombre}' no cumple las condiciones de peso/edad para su categoría.");
            }

            return potrero.anadir_res(res);
        }

        static void EjecutarCaso(string id, Func<string> accion) {

            try {
                string resultado = accion();
                Console.WriteLine($"{id} -> RESULTADO: {resultado}\n");
            }
            catch (Exception ex) {
                Console.WriteLine($"{id} -> EXCEPCIÓN: {ex.Message}\n");
            }
        }
    }
}