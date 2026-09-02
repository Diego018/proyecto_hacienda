using Bib_Hacienda.Clases;
using p_mvcHacienda.Infraestructura.puertos;
using p_mvcHacienda.Servicios.contratos;

namespace p_mvcHacienda.Servicios {

    public class UsuarioService : IUsuarioService {

        private readonly IPersistenciaUsuarios _usuarioPersistencia;

        public UsuarioService(IPersistenciaUsuarios persistencia) {
            
            _usuarioPersistencia = persistencia;
            
        }

        public string CrearUsuario(string nombre, string contrasena) {

            try {
                    
                if (string.IsNullOrWhiteSpace(nombre)) {
                    throw new ArgumentException("El nombre no puede estar vacío.");
                }

                if (string.IsNullOrWhiteSpace(contrasena)) {
                    throw new ArgumentException("La contraseña no puede estar vacía.");
                }

                var usuarios = _usuarioPersistencia.CargarUsuarios();

                if (usuarios.Any(u => u.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))) {
                    throw new InvalidOperationException($"Ya existe un usuario con el nombre '{nombre}'.");
                }

                Usuario nuevoUsuario = new Usuario(nombre, contrasena);
                _usuarioPersistencia.GuardarUsuario(nuevoUsuario);

                return $"Usuario '{nombre}' creado exitosamente.";
            }
            catch (Exception ex) {
                throw new Exception($"Error al crear el usuario: {ex.Message}");
            }
        }

        public List<Usuario> ObtenerTodosLosUsuarios() {

            return _usuarioPersistencia.CargarUsuarios().OrderBy(u => u.Nombre).ToList();
        }
    }
}