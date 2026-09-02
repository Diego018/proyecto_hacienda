using Bib_Hacienda.Clases;

namespace p_mvcHacienda.Servicios.Contratos {

    public interface IPotreroService {

        string CrearPotrero(string identificacion, l_tipos_potreros tipo);

        List<Potrero> ObtenerTodosLosPotreros();

        Potrero ObtenerPotreroPorIdentificacion(string id);

        string AgregarRes(string potreroId, Res res);
        
    }
    
}