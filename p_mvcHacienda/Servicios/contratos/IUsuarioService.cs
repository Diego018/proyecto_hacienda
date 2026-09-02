using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Servicios.contratos;

public interface IUsuarioService {
    
    string CrearUsuario(string nombre, string contrasena);

    List<Usuario> ObtenerTodosLosUsuarios();
    
    
    
}