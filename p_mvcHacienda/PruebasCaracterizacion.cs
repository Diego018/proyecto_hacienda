using System;
using Bib_Hacienda.Clases;
using Bib_Hacienda.Reglas;
using Bib_Hacienda.Clases.Estrategias;
using Bib_Hacienda.Clases.Derivados;
using Bib_Hacienda.Clases.Factories;
using Bib_Hacienda.Clases.Validaciones;
using Bib_Hacienda.Eventos; 

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

            // 1. Ensamblaje de la Cadena de Responsabilidad (Chain of Responsibility)
            ValidadorHandler cadenaValidacion = new ValidarPotreroHandler();
            cadenaValidacion.SetNext(new ValidarResHandler());

            // 2. Ejecución fluida
            cadenaValidacion.Validar(potrero);
            cadenaValidacion.Validar(res);

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

        // Prueba de strategy pattern para el cálculo de ventas
        public static void ProbarPatronStrategyVentas()
        {
            Console.WriteLine("\n===========================================");
            Console.WriteLine("PRUEBA DE CARACTERIZACIÓN: PATRÓN STRATEGY");
            Console.WriteLine("===========================================");

            try 
            {
                // 1. Simular la Res (Cebon de 400kg). 
                Res miCebon = new Cebon("Toro Loco", 400, DateTime.Now.AddYears(-3));

                // 2. El cálculo TO-BE 
                Venta ventaToBe = new Venta(null, null, DateTime.Now, miCebon, 0);
                
                // Inyectamos la estrategia
                ventaToBe.ProcesarVenta(new EstrategiaVentaCebon());

                Console.WriteLine($"Monto Calculado por Strategy (TO-BE): {ventaToBe.Monto}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR COMPILANDO LA PRUEBA: {ex.Message}");
            }
            
            Console.WriteLine("===========================================\n");
        }

        
        // Prueba de abstract factory pattern para productos ganaderos
        public static void ProbarPatronAbstractFactoryProductosGanaderos()
        {
            Console.WriteLine("\n====================================================");
            Console.WriteLine("PRUEBA DE CARACTERIZACIÓN: PATRÓN ABSTRACT FACTORY");
            Console.WriteLine("====================================================");

            try 
            {
                // 1. Instanciamos las fábricas concretas mediante su abstracción base (DIP)
                ProductoGanaderoFactory fabricaLeche = new LecheFactory(3.8f);
                ProductoGanaderoFactory fabricaCarne = new CarneFactory(5, "Lomo Fino");
                ProductoGanaderoFactory fabricaPiel = new PielFactory(3.0f, "A");

                // 2. Delegamos la creación sin usar 'new' sobre los productos concretos
                ProductoGanadero productoLeche = fabricaLeche.CrearProductoGanadero("L01", "Leche Entera", 3200);
                ProductoGanadero productoCarne = fabricaCarne.CrearProductoGanadero("C01", "Carne Premium", 28000);
                ProductoGanadero productoPiel = fabricaPiel.CrearProductoGanadero("P01", "Piel Curtida", 45000);

                // 3. Imprimimos el resultado directamente en la consola (Línea 146 corregida)
                Console.WriteLine($"ÉXITO (Abstract Factory). Productos creados dinámicamente:\n" +
                                  $"  - {productoLeche.Nombre} (${productoLeche.CalcularPrecio()})\n" +
                                  $"  - {productoCarne.Nombre} (${productoCarne.CalcularPrecio()})\n" +
                                  $"  - {productoPiel.Nombre} (${productoPiel.CalcularPrecio()})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR COMPILANDO LA PRUEBA: {ex.Message}");
            }
            
            Console.WriteLine("====================================================\n");
        }

        // Prueba de abstract factory pattern para reses
        public static void ProbarPatronFactoryMethodReses()
        {
            Console.WriteLine("\n====================================================");
            Console.WriteLine("PRUEBA DE CARACTERIZACIÓN: FACTORY METHOD (RESES)");
            Console.WriteLine("====================================================");

            try 
            {
                // 1. Instanciamos las fábricas usando la abstracción estricta del UML
                ResFactory fabricaTernero = new TerneroFactory();
                ResFactory fabricaNovillo = new NovilloFactory();
                ResFactory fabricaCebon = new CebonFactory();

                // 2. Delegamos la creación para evitar el 'new Ternero()' directo en Web
                Res miTernero = fabricaTernero.CrearRes("Toro1", 200, DateTime.Now.AddMonths(-10));
                Res miNovillo = fabricaNovillo.CrearRes("Toro2", 450, DateTime.Now.AddMonths(-55));
                Res miCebon = fabricaCebon.CrearRes("Toro3", 350, DateTime.Now.AddMonths(-30));

                // 3. Verificamos que las entidades creadas por la fábrica pasen la validación AS-IS
                Potrero p = new Potrero("P_PRUEBA", l_tipos_potreros.Ternero);
                AgregarConValidacion(p, miTernero); // Validará el peso y edad

                Console.WriteLine($"ÉXITO (ResFactory). Reses creadas polimórficamente:\n" +
                                  $"  - {miTernero.Nombre} (Tipo instanciado: {miTernero.GetType().Name})\n" +
                                  $"  - {miNovillo.Nombre} (Tipo instanciado: {miNovillo.GetType().Name})\n" +
                                  $"  - {miCebon.Nombre} (Tipo instanciado: {miCebon.GetType().Name})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR COMPILANDO LA PRUEBA: {ex.Message}");
            }
            
            Console.WriteLine("====================================================\n");
        }

        // Prueba de abstract factory pattern para vacunas
        public static void ProbarPatronAbstractFactoryVacunas()
        {
            Console.WriteLine("\n====================================================");
            Console.WriteLine("PRUEBA DE CARACTERIZACIÓN: PATRÓN ABSTRACT FACTORY (VACUNAS)");
            Console.WriteLine("====================================================");

            try 
            {
               VacunaFactory fabricaSanidad = new VacunaFactory();
                DateTime hoy = DateTime.Now;

                // 1. Instanciamos Viva 
                Vacuna vacunaViva = fabricaSanidad.CrearVacunaViva("Aftosa-V", "L-99", hoy.AddYears(1), hoy, Viva.enum_l_atenuaciones.Atenuacion20);
                
                // 2. Instanciamos Bacteriana.
                uint periodoPrueba = 3; 
                Vacuna vacunaBacteriana = fabricaSanidad.CrearVacunaBacteriana("Triple-B", "L-88", hoy.AddMonths(6), hoy, periodoPrueba);

                Console.WriteLine($"ÉXITO (VacunaFactory). Vacunas creadas:\n" +
                                  $"  - {vacunaViva.Nombre} (Atenuación: {((Viva)vacunaViva).Periodo_atenuacion})\n" +
                                  $"  - {vacunaBacteriana.Nombre} (Periodo Aplicación: {((Bacteriana)vacunaBacteriana).Periodo_aplicacion} semanas)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR COMPILANDO LA PRUEBA: {ex.Message}");
            }
            
            Console.WriteLine("====================================================\n");
        }

        // Prueba de observer pattern
        public static void ProbarPatronObserver()
        {
            Console.WriteLine("\n====================================================");
            Console.WriteLine("PRUEBA DE CARACTERIZACIÓN: PATRÓN OBSERVER");
            Console.WriteLine("====================================================");

            try 
            {
                // 1. Instanciamos la entidad de dominio (Sujeto emisor)
                Potrero potreroPrueba = new Potrero("POT-OBSERVER-01", l_tipos_potreros.Novillo);

                // 2. Suscribimos el evento nativo a la Regla de Negocio (Observador)
                potreroPrueba.PotreroLleno += ReglaPotrero.OnPotreroLleno;

                Console.WriteLine($"[TEST] Potrero '{potreroPrueba.Identificacion}' creado.");
                Console.WriteLine("[TEST] Suscripción realizada: PotreroLleno -> ReglaPotrero.OnPotreroLleno");
                Console.WriteLine("[TEST] Disparando el evento directamente a través del método de dominio...\n");

                // 3. Disparamos directamente el evento enviando sus EventArgs específicos
                potreroPrueba.OnPotreroLleno(new PotreroEventArgs(potreroPrueba));

                Console.WriteLine("ÉXITO: El patrón Observer funcionó correctamente sin acoplamiento.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR EN LA PRUEBA DEL OBSERVER: {ex.Message}");
            }

            Console.WriteLine("====================================================\n");
        }
    }
}