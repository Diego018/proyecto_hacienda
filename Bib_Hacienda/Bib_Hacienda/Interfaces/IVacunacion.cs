using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Interfaces
{
    public interface IVacunacion
    {
        //Metodo para aplicar vacuna
        string aplicar_vacuna(Vacuna vacuna, string nombre, string id_potrero);
    }
}
