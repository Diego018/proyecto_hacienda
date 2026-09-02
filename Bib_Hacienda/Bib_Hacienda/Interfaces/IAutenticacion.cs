using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Interfaces
{
    //Interfaz para autenticar y autorizar operaciones de usuarios
    public interface IAutenticacion
    {
        //Autoriza la ejecución de una operación para un usuario
        void AutorizarOperacion(Usuario usuario, string operacion);
  }
}
