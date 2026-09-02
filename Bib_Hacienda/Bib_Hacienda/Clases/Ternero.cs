using Bib_Hacienda.Reglas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Hacienda.Clases
{
    public class Ternero : Res {

        // Constructor
        public Ternero(string nombre, uint peso, DateTime fechaNacimiento)
            : base(nombre, peso, fechaNacimiento) {}
        
        public override bool ValidarCrecimiento() {
            
            return Peso >= ReglaRes.peso_min_ternero && Edad() <= ReglaRes.edad_max_ternero;
            
        }
        
        //metodos sobrescritos de los eventos
        
        public override bool EstaEnPesoMinimo()
        {
            
            return Peso < ReglaRes.peso_min_ternero;
            
        }

        public override bool EstaAptaParaVenta()
        {
            
            return Peso >= ReglaRes.peso_recom_venta_ternero;
            
        }
        
    }
    
}
