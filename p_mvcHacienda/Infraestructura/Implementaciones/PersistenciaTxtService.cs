using System.Globalization;
using Bib_Hacienda.Clases;
using p_mvcHacienda.Infraestructura.puertos;

namespace p_mvcHacienda.Infraestructura.Implementaciones {

    public class PersistenciaTxtService : IPersistenciaHacienda, IPersistenciaVentas, IPersistenciaUsuarios {

        private readonly string _directorioBase;

        public PersistenciaTxtService(string directorio) {

            _directorioBase = directorio;

            if (!Directory.Exists(_directorioBase)) {
                Directory.CreateDirectory(_directorioBase);
            }
        }

        public Hacienda CargarHacienda() {

            string ruta = Path.Combine(_directorioBase, "Hacienda.txt");
            var hacienda = new Hacienda("H1", "Hacienda Principal");

            if (!File.Exists(ruta)) return hacienda;

            foreach (var linea in File.ReadAllLines(ruta)) {

                if (string.IsNullOrWhiteSpace(linea)) continue;

                var partes = linea.Split('|');
                string prefijo = partes[0];

                if (prefijo == "#POTRERO") {
                    hacienda.crear_potrero(partes[1], Enum.Parse<l_tipos_potreros>(partes[2]));
                }
                else if (prefijo == "#RES") {
                    string idPotrero = partes[1];
                    string nombre = partes[2];
                    uint peso = uint.Parse(partes[3]);
                    DateTime fechaNacimiento = DateTime.ParseExact(partes[4], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    string tipoRes = partes[5];

                    Potrero potrero = hacienda.buscar_potrero(idPotrero);
                    if (potrero == null) continue;

                    Res res = tipoRes switch {
                        "Ternero" => new Ternero(nombre, peso, fechaNacimiento),
                        "Cebon" => new Cebon(nombre, peso, fechaNacimiento),
                        "Novillo" => new Novillo(nombre, peso, fechaNacimiento),
                        _ => throw new Exception($"Tipo de res desconocido: {tipoRes}")
                    };

                    potrero.anadir_res(res);
                }
                else if (prefijo == "#VACUNA_APLICADA") {
                    string idPotrero = partes[1];
                    string nombreRes = partes[2];
                    string nombreVacuna = partes[3];
                    string lote = partes[4];
                    DateTime fechaVenc = DateTime.ParseExact(partes[5], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime fechaAplic = DateTime.ParseExact(partes[6], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    string tipoVacuna = partes[7];
                    string valorExtra = partes[8];

                    Potrero potrero = hacienda.buscar_potrero(idPotrero);
                    if (potrero == null) continue;

                    Res res = potrero.buscar_res(nombreRes);
                    if (res == null) continue;

                    Vacuna vacuna = tipoVacuna == "Bacteriana"
                        ? new Bacteriana(nombreVacuna, lote, fechaVenc, fechaAplic, uint.Parse(valorExtra))
                        : new Viva(nombreVacuna, lote, fechaVenc, fechaAplic, Enum.Parse<Viva.enum_l_atenuaciones>(valorExtra));

                    res.agregarVacuna(vacuna);
                }
            }

            return hacienda;
        }

        public void GuardarHacienda(Hacienda hacienda) {

            string ruta = Path.Combine(_directorioBase, "Hacienda.txt");
            var lineas = new List<string>();

            foreach (var potrero in hacienda.obtener_potreros()) {

                lineas.Add($"#POTRERO|{potrero.Identificacion}|{potrero.Tipo_potrero}");

                foreach (var res in potrero.obtener_reses()) {

                    string tipoRes = res.GetType().Name;
                    string fechaNac = res.FechaNacimiento.ToString("yyyy-MM-dd");

                    lineas.Add($"#RES|{potrero.Identificacion}|{res.Nombre}|{res.Peso}|{fechaNac}|{tipoRes}");

                    foreach (var vacuna in res.VacunasAplicadas) {

                        string tipoVacuna = vacuna.GetType().Name;
                        string fechaVenc = vacuna.Fecha_vencimiento.ToString("yyyy-MM-dd");
                        string fechaAplic = vacuna.Fecha_aplicacion.ToString("yyyy-MM-dd");
                        string valorExtra = vacuna is Bacteriana bac ? bac.Periodo_aplicacion.ToString() : ((Viva)vacuna).Periodo_atenuacion.ToString();

                        lineas.Add($"#VACUNA_APLICADA|{potrero.Identificacion}|{res.Nombre}|{vacuna.Nombre}|{vacuna.Lote}|{fechaVenc}|{fechaAplic}|{tipoVacuna}|{valorExtra}");
                    }
                }
            }

            File.WriteAllLines(ruta, lineas);
        }

        public List<Venta> CargarVentas() {

            string ruta = Path.Combine(_directorioBase, "Ventas.txt");
            var ventas = new List<Venta>();

            if (!File.Exists(ruta)) return ventas;

            foreach (var linea in File.ReadAllLines(ruta)) {

                if (string.IsNullOrWhiteSpace(linea)) continue;

                var partes = linea.Split('|');

                string idPotrero = partes[0];
                l_tipos_potreros tipoPotrero = Enum.Parse<l_tipos_potreros>(partes[1]);
                DateTime fecha = DateTime.ParseExact(partes[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string nombreRes = partes[3];
                uint pesoRes = uint.Parse(partes[4]);
                DateTime fechaNacimientoRes = DateTime.ParseExact(partes[5], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string tipoRes = partes[6];
                uint monto = uint.Parse(partes[7]);
                string nombreUsuario = partes[8];

                Potrero potrero = new Potrero(idPotrero, tipoPotrero);

                Res res = tipoRes switch {
                    "Ternero" => new Ternero(nombreRes, pesoRes, fechaNacimientoRes),
                    "Cebon" => new Cebon(nombreRes, pesoRes, fechaNacimientoRes),
                    "Novillo" => new Novillo(nombreRes, pesoRes, fechaNacimientoRes),
                    _ => throw new Exception($"Tipo de res desconocido: {tipoRes}")
                };

                Usuario usuario = new Usuario(nombreUsuario, "");

                ventas.Add(new Venta(usuario, potrero, fecha, res, monto));
            }

            return ventas;
        }

        public void GuardarVenta(Venta venta) {

            string ruta = Path.Combine(_directorioBase, "Ventas.txt");
            string fecha = venta.Fecha.ToString("yyyy-MM-dd");
            string fechaNacRes = venta.Res.FechaNacimiento.ToString("yyyy-MM-dd");
            string tipoRes = venta.Res.GetType().Name;
            string tipoPotrero = venta.Potrero.Tipo_potrero.ToString();

            string linea = $"{venta.Potrero.Identificacion}|{tipoPotrero}|{fecha}|{venta.Res.Nombre}|{venta.Res.Peso}|{fechaNacRes}|{tipoRes}|{venta.Monto}|{venta.Usuario.Nombre}";

            File.AppendAllLines(ruta, new[] { linea });
        }

        public List<Usuario> CargarUsuarios() {

            string ruta = Path.Combine(_directorioBase, "Usuarios.txt");
            var usuarios = new List<Usuario>();

            if (!File.Exists(ruta)) return usuarios;

            foreach (var linea in File.ReadAllLines(ruta)) {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var partes = linea.Split('|');
                usuarios.Add(new Usuario(partes[0], partes[1]));
            }

            return usuarios;
        }

        public void GuardarUsuario(Usuario usuario) {

            string ruta = Path.Combine(_directorioBase, "Usuarios.txt");
            string linea = $"{usuario.Nombre}|{usuario.Contrasena}";
            File.AppendAllLines(ruta, new[] { linea });
        }
    }
}