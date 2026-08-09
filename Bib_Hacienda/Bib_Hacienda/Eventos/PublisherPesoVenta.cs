using System;
using Bib_Hacienda.Clases;

namespace Bib_Hacienda.Eventos {
    
    public class PublisherPesoVenta {
        
        public delegate void dele_peso_venta(string peso_venta);
        public event dele_peso_venta evt_peso_venta;

        public void Informar_Peso_Venta(Res res) {
            
            try {
                
                if (res.EstaAptaParaVenta()) {
                    
                    string mensaje = $"[Evento] La res '{res.Nombre}' tiene un peso {res.Peso}, apta para venta.";
                    evt_peso_venta?.Invoke(mensaje);
                    
                }
                
            } catch (Exception er) {
                
                throw new Exception("Error inesperado en el metodo Informar_Peso_Venta: " + er.Message);
                
            }
            
        }
        
    }
    
}