using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Servicios.contratos;

public interface IAutenticacionService {
    
    bool ValidarCredenciales(string nombre, string contrasena);

    void AutorizarOperacion(Usuario usuario, string operacion);
    
}