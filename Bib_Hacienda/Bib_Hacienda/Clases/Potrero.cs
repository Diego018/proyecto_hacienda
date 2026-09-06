using System;
using System.Collections.Generic;
using System.Linq;
using Bib_Hacienda.Eventos;

namespace Bib_Hacienda.Clases
{
    public class Potrero
    {
        private string identificacion;
        private l_tipos_potreros tipo_potrero;
        private List<Res> l_reses;

        public string Identificacion => identificacion;
        public l_tipos_potreros Tipo_potrero => tipo_potrero;

        public Potrero(string id, l_tipos_potreros tipo)
        {
            identificacion = id;
            tipo_potrero = tipo;
            l_reses = new List<Res>();
        }

        public string anadir_res(Res res) {

            if (res == null) {

                throw new ArgumentNullException(nameof(res), "La res no puede ser null.");
                
            }
            
            l_reses.Add(res);
            return $"La res '{res.Nombre}' fue añadida al potrero '{identificacion}'.";
            
        }

        public Res buscar_res(string nombre) {
            
            return l_reses.FirstOrDefault(r => r.Nombre == nombre);
            
        }

        public string eliminar_res(string nombre) { 
            
            Res res = buscar_res(nombre);
            
            if (res == null) {
            
                return $"No se encontró la res '{nombre}' en el potrero '{identificacion}'.";
                
            }
            
            l_reses.Remove(res);
            return $"La res '{nombre}' fue eliminada del potrero '{identificacion}'.";
        }

        public List<Res> obtener_reses() {
        
            return new List<Res>(l_reses);
            
        }

        // --- PATRÓN OBSERVER: EVENTOS NATIVOS ---
        public event EventHandler<PotreroEventArgs> PotreroLleno;
        public event EventHandler<PotreroEventArgs> PotreroMitad;

        public virtual void OnPotreroLleno(PotreroEventArgs e) 
        {
            PotreroLleno?.Invoke(this, e);
        }

        public virtual void OnPotreroMitad(PotreroEventArgs e) 
        {
            PotreroMitad?.Invoke(this, e);
        }
        
    }
    
}