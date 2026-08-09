using System;

namespace Bib_Hacienda.Clases
{
    public abstract class RegistroClinico {
        
        private DateTime fecha;
        private string observacion;

        protected RegistroClinico(DateTime fecha, string observacion) {
            
            this.fecha = fecha;
            this.observacion = observacion;
            
        }

        public DateTime Fecha => fecha;
        public string Observacion => observacion;
    }
}
