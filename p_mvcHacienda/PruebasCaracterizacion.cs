using System;
using Bib_Hacienda.Clases;
using Bib_Hacienda.Reglas;
using Bib_Hacienda.Clases.Estrategias;
using Bib_Hacienda.Clases.Derivados;
using Bib_Hacienda.Clases.Factories;
using Bib_Hacienda.Clases.Validaciones;
using Bib_Hacienda.Eventos;

namespace p_mvcHacienda
{
    public static class PruebasCaracterizacion
    {
        // =========================================================================
        // SUITE DE COMPARACIÓN AUTOMÁTICA: AS-IS vs TO-BE
        // =========================================================================
        public static void EjecutarCasosASIS()
        {
            Console.WriteLine("=====================================================================");
            Console.WriteLine("  EVIDENCIA DE REGRESIÓN: AUDITORÍA DE COMPORTAMIENTO (AS-IS vs TO-BE)");
            Console.WriteLine("=====================================================================\n");

            // Caso CC-01
            ValidarYComparar(
                id: "CC-01",
                descripcion: "Añadir Ternero Vaca1 válido a Potrero P1",
                esperado: "La res 'Vaca1' fue añadida al potrero 'P1'.",
                accion: () => {
                    Potrero p = new Potrero("P1", l_tipos_potreros.Ternero);
                    return AgregarConValidacion(p, new Ternero("Vaca1", 200, DateTime.Now.AddMonths(-10)));
                }
            );

            // Caso CC-02
            ValidarYComparar(
                id: "CC-02",
                descripcion: "Rechazar Ternero Vaca2 con edad inválida para categoría",
                esperado: "EXCEPCION: La res 'Vaca2' no cumple las condiciones de peso/edad para su categoría.",
                accion: () => {
                    Potrero p = new Potrero("P1", l_tipos_potreros.Ternero);
                    return AgregarConValidacion(p, new Ternero("Vaca2", 200, DateTime.Now.AddMonths(-20)));
                }
            );

            // Caso CC-03
            ValidarYComparar(
                id: "CC-03",
                descripcion: "Añadir Cebón Vaca3 válido a Potrero P2",
                esperado: "La res 'Vaca3' fue añadida al potrero 'P2'.",
                accion: () => {
                    Potrero p = new Potrero("P2", l_tipos_potreros.Cebon);
                    return AgregarConValidacion(p, new Cebon("Vaca3", 350, DateTime.Now.AddMonths(-30)));
                }
            );

            // Caso CC-04
            ValidarYComparar(
                id: "CC-04",
                descripcion: "Rechazar Cebón Vaca4 por edad fuera de rango",
                esperado: "EXCEPCION: La res 'Vaca4' no cumple las condiciones de peso/edad para su categoría.",
                accion: () => {
                    Potrero p = new Potrero("P2", l_tipos_potreros.Cebon);
                    return AgregarConValidacion(p, new Cebon("Vaca4", 350, DateTime.Now.AddMonths(-5)));
                }
            );

            // Caso CC-05
            ValidarYComparar(
                id: "CC-05",
                descripcion: "Añadir Novillo Vaca5 válido a Potrero P3",
                esperado: "La res 'Vaca5' fue añadida al potrero 'P3'.",
                accion: () => {
                    Potrero p = new Potrero("P3", l_tipos_potreros.Novillo);
                    return AgregarConValidacion(p, new Novillo("Vaca5", 450, DateTime.Now.AddMonths(-55)));
                }
            );

            // Caso CC-06
            ValidarYComparar(
                id: "CC-06",
                descripcion: "Rechazar ingreso por capacidad máxima alcanzada en Potrero P4",
                esperado: $"EXCEPCION: El potrero 'P4' alcanzó su capacidad máxima ({ReglaPotrero.max_reses_potrero} reses).",
                accion: () => {
                    Potrero p = new Potrero("P4", l_tipos_potreros.Novillo);
                    // Llenamos el potrero hasta su límite estricto de negocio (50 reses)
                    for (int i = 0; i < ReglaPotrero.max_reses_potrero; i++) {
                        AgregarConValidacion(p, new Novillo($"Res{i}", 450, DateTime.Now.AddMonths(-55)));
                    }
                    // La res número 51 detonará el Handler con la excepción exacta
                    return AgregarConValidacion(p, new Novillo("VacaExtra", 450, DateTime.Now.AddMonths(-55)));
                }
            );

            // Caso CC-07
            ValidarYComparar(
                id: "CC-07",
                descripcion: "Rechazar res con nombre vacío",
                esperado: "EXCEPCION: El nombre de la res no puede estar vacío.",
                accion: () => {
                    Potrero p = new Potrero("P1", l_tipos_potreros.Ternero);
                    return AgregarConValidacion(p, new Ternero("", 200, DateTime.Now.AddMonths(-10)));
                }
            );

            // Caso CC-08
            ValidarYComparar(
                id: "CC-08",
                descripcion: "Búsqueda y consulta de atributos de Vaca1",
                esperado: "Res encontrada: Vaca1, Edad: 10, Peso: 200",
                accion: () => {
                    Potrero p = new Potrero("P1", l_tipos_potreros.Ternero);
                    AgregarConValidacion(p, new Ternero("Vaca1", 200, DateTime.Now.AddMonths(-10)));
                    Res encontrada = p.buscar_res("Vaca1");
                    return $"Res encontrada: {encontrada.Nombre}, Edad: {encontrada.Edad()}, Peso: {encontrada.Peso}";
                }
            );

            Console.WriteLine("=====================================================================\n");
        }

        // --- ORQUESTADOR CON CHAIN OF RESPONSIBILITY ---
        static string AgregarConValidacion(Potrero potrero, Res res) 
        {
            if (res == null || string.IsNullOrWhiteSpace(res.Nombre)) {
                throw new ArgumentException("El nombre de la res no puede estar vacío.");
            }
            if (potrero.buscar_res(res.Nombre) != null) {
                throw new InvalidOperationException($"Ya existe una res con el nombre '{res.Nombre}' en el potrero '{potrero.Identificacion}'");
            }

            // Invocación del Patrón Handler
            ValidadorHandler cadenaValidacion = new ValidarPotreroHandler();
            cadenaValidacion.SetNext(new ValidarResHandler());

            cadenaValidacion.Validar(potrero);
            cadenaValidacion.Validar(res);

            return potrero.anadir_res(res);
        }

        // --- MOTOR DE ASERSION Y AUDITORÍA DE SALIDAS ---
        static void ValidarYComparar(string id, string descripcion, string esperado, Func<string> accion) 
        {
            string obtenido = "";
            try {
                obtenido = accion();
            }
            catch (Exception ex) {
                obtenido = $"EXCEPCION: {ex.Message}";
            }

            // Normalización básica para ignorar diferencias diminutas de espacios
            bool coinciden = obtenido.Trim().Equals(esperado.Trim(), StringComparison.OrdinalIgnoreCase);

            if (coinciden)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[✓ PASÓ - AS-IS CONSERVADO] {id}: {descripcion}");
                Console.ResetColor();
                Console.WriteLine($"   --> Resultado: {obtenido}\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[X FALLÓ - REGRESIÓN DETECTADA] {id}: {descripcion}");
                Console.ResetColor();
                Console.WriteLine($"   --> Esperado: {esperado}");
                Console.WriteLine($"   --> Obtenido: {obtenido}\n");
            }
        }

        // =========================================================================
        // DEMOSTRACIÓN DE PATRONES SOLID (DEMOS DE EXTENSIBILIDAD)
        // =========================================================================

        public static void ProbarPatronStrategyVentas()
        {
            Console.WriteLine("\n===========================================");
            Console.WriteLine("PRUEBA DE CARACTERIZACIÓN: PATRÓN STRATEGY");
            Console.WriteLine("===========================================");

            try 
            {
                Res miCebon = new Cebon("Toro Loco", 400, DateTime.Now.AddYears(-3));
                Venta ventaToBe = new Venta(null, null, DateTime.Now, miCebon, 0);
                
                ventaToBe.ProcesarVenta(new EstrategiaVentaCebon());

                Console.WriteLine($"Monto Calculado por Strategy (TO-BE): {ventaToBe.Monto}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR EN LA PRUEBA STRATEGY: {ex.Message}");
            }
            
            Console.WriteLine("===========================================\n");
        }

        public static void ProbarPatronAbstractFactoryProductosGanaderos()
        {
            Console.WriteLine("\n====================================================");
            Console.WriteLine("PRUEBA DE CARACTERIZACIÓN: PATRÓN ABSTRACT FACTORY");
            Console.WriteLine("====================================================");

            try 
            {
                ProductoGanaderoFactory fabricaLeche = new LecheFactory(3.8f);
                ProductoGanaderoFactory fabricaCarne = new CarneFactory(5, "Lomo Fino");
                ProductoGanaderoFactory fabricaPiel = new PielFactory(3.0f, "A");

                ProductoGanadero productoLeche = fabricaLeche.CrearProductoGanadero("L01", "Leche Entera", 3200);
                ProductoGanadero productoCarne = fabricaCarne.CrearProductoGanadero("C01", "Carne Premium", 28000);
                ProductoGanadero productoPiel = fabricaPiel.CrearProductoGanadero("P01", "Piel Curtida", 45000);

                Console.WriteLine($"ÉXITO (Abstract Factory). Productos creados dinámicamente:\n" +
                                  $"  - {productoLeche.Nombre} (${productoLeche.CalcularPrecio()})\n" +
                                  $"  - {productoCarne.Nombre} (${productoCarne.CalcularPrecio()})\n" +
                                  $"  - {productoPiel.Nombre} (${productoPiel.CalcularPrecio()})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR EN ABSTRACT FACTORY: {ex.Message}");
            }
            
            Console.WriteLine("====================================================\n");
        }

        public static void ProbarPatronFactoryMethodReses()
        {
            Console.WriteLine("\n====================================================");
            Console.WriteLine("PRUEBA DE CARACTERIZACIÓN: FACTORY METHOD (RESES)");
            Console.WriteLine("====================================================");

            try 
            {
                ResFactory fabricaTernero = new TerneroFactory();
                ResFactory fabricaNovillo = new NovilloFactory();
                ResFactory fabricaCebon = new CebonFactory();

                Res miTernero = fabricaTernero.CrearRes("Toro1", 200, DateTime.Now.AddMonths(-10));
                Res miNovillo = fabricaNovillo.CrearRes("Toro2", 450, DateTime.Now.AddMonths(-55));
                Res miCebon = fabricaCebon.CrearRes("Toro3", 350, DateTime.Now.AddMonths(-30));

                Console.WriteLine($"ÉXITO (ResFactory). Reses creadas polimórficamente:\n" +
                                  $"  - {miTernero.Nombre} (Tipo instanciado: {miTernero.GetType().Name})\n" +
                                  $"  - {miNovillo.Nombre} (Tipo instanciado: {miNovillo.GetType().Name})\n" +
                                  $"  - {miCebon.Nombre} (Tipo instanciado: {miCebon.GetType().Name})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR EN FACTORY METHOD: {ex.Message}");
            }
            
            Console.WriteLine("====================================================\n");
        }

        public static void ProbarPatronAbstractFactoryVacunas()
        {
            Console.WriteLine("\n====================================================");
            Console.WriteLine("PRUEBA DE CARACTERIZACIÓN: PATRÓN SIMPLE FACTORY (VACUNAS)");
            Console.WriteLine("====================================================");

            try 
            {
                VacunaFactory fabricaSanidad = new VacunaFactory();
                DateTime hoy = DateTime.Now;

                Vacuna vacunaViva = fabricaSanidad.CrearVacunaViva("Aftosa-V", "L-99", hoy.AddYears(1), hoy, Viva.enum_l_atenuaciones.Atenuacion20);
                uint periodoPrueba = 3; 
                Vacuna vacunaBacteriana = fabricaSanidad.CrearVacunaBacteriana("Triple-B", "L-88", hoy.AddMonths(6), hoy, periodoPrueba);

                Console.WriteLine($"ÉXITO (VacunaFactory). Vacunas creadas:\n" +
                                  $"  - {vacunaViva.Nombre} (Atenuación: {((Viva)vacunaViva).Periodo_atenuacion})\n" +
                                  $"  - {vacunaBacteriana.Nombre} (Periodo Aplicación: {((Bacteriana)vacunaBacteriana).Periodo_aplicacion} semanas)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR EN VACUNA FACTORY: {ex.Message}");
            }
            
            Console.WriteLine("====================================================\n");
        }

        public static void ProbarPatronObserver()
        {
            Console.WriteLine("\n====================================================");
            Console.WriteLine("PRUEBA DE CARACTERIZACIÓN: PATRÓN OBSERVER");
            Console.WriteLine("====================================================");

            try 
            {
                Potrero potreroPrueba = new Potrero("POT-OBSERVER-01", l_tipos_potreros.Novillo);
                potreroPrueba.PotreroLleno += ReglaPotrero.OnPotreroLleno;

                Console.WriteLine($"[TEST] Potrero '{potreroPrueba.Identificacion}' creado.");
                Console.WriteLine("[TEST] Suscripción realizada: PotreroLleno -> ReglaPotrero.OnPotreroLleno");
                Console.WriteLine("[TEST] Disparando el evento directamente a través del método de dominio...\n");

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