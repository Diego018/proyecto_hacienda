using System.Collections.Generic;
using System.Linq;

namespace Bib_Hacienda.Clases
{
    public class Hacienda
    {
        private string id;
        private string nombre;
        private List<Potrero> l_potreros;

        public Hacienda (string id, string nombre) {
            
            this.id = id;
            this.nombre = nombre;
            l_potreros = new List<Potrero>();
            
        }

        public string crear_potrero(string id, l_tipos_potreros tipo) {
            
            Potrero nuevo_potrero = new Potrero(id, tipo); //no violo dip ya que no estoy instanciando algo especifico
            l_potreros.Add(nuevo_potrero);
            return $"El potrero '{id}' fue añadido a la hacienda.";
            
        }

        public Potrero buscar_potrero(string id)  {
            
            return l_potreros.FirstOrDefault(p => p.Identificacion == id);
            
        }

        public List<Potrero> obtener_potreros() {
            
            return new List<Potrero>(l_potreros);
            
        }
        
    }
    
}