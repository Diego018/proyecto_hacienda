using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Infraestructura.puertos;

public interface IPersistenciaUsuarios {
    
    List<Usuario> CargarUsuarios();

    void GuardarUsuario(Usuario usuario);
    
}