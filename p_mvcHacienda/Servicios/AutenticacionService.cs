using Bib_Hacienda.Clases;
using p_mvcHacienda.Infraestructura.puertos;
using p_mvcHacienda.Servicios.contratos;

namespace p_mvcHacienda.Servicios {

    public class AutenticacionService : IAutenticacionService {

        private readonly IPersistenciaUsuarios _usuarioPersistencia;

        public AutenticacionService(IPersistenciaUsuarios persistencia) {
            _usuarioPersistencia = persistencia;
        }

        public bool ValidarCredenciales(string nombre, string contrasena) {

            var usuarios = _usuarioPersistencia.CargarUsuarios();

            return usuarios.Any(u => u.Nombre == nombre && u.Contrasena == contrasena);
        }

        public void AutorizarOperacion(Usuario usuario, string operacion) {
            
            throw new NotImplementedException();
            
        }
    }
}