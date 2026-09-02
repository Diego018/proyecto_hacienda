using System;
using System.Collections.Generic;

namespace Bib_Hacienda.Clases
{
    public class HistoriaClinica {
        
        private string Id;
        private DateTime FechaCreacion;
        private List<RegistroClinico> Registros;

        public HistoriaClinica (string id) {

            Id = id;
            FechaCreacion = DateTime.Now;
            Registros = new List<RegistroClinico>();

        }

        public void AgregarRegistro (RegistroClinico registro)
        {
            
            Registros.Add(registro);

        }

        public List<RegistroClinico> ObtenerHistorial() {
            
            return new List<RegistroClinico>(Registros);
            
        }
        
    }
    
}
