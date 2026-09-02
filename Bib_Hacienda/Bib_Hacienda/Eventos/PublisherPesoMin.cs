using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Eventos {
    public class PublisherPesoMin {
        
        public delegate void dele_peso_min(string peso_min);
        public event dele_peso_min evt_peso_min;

        public void Informar_Peso_Min(Res res) {
            
            try {
                
                if (res.EstaEnPesoMinimo()) {
                    
                    string mensaje = $"[Evento] La res '{res.Nombre}' tiene un peso {res.Peso}, está en desnutrición.";
                    evt_peso_min?.Invoke(mensaje);
                    
                }
                
            } catch (Exception er) {
                
                throw new Exception("[Evento] Error inesperado en el metodo Informar_Peso_Min: " + er.Message);
                
            }
            
        }
        
    }
    
}