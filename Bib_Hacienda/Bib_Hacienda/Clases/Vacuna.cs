using System;

namespace Bib_Hacienda.Clases
{
    public abstract class Vacuna
    {

        //Atributos
        private string nombre;
        private string lote;
        private DateTime fecha_vencimiento;
        private DateTime fecha_aplicacion;

        //Constructor
        protected Vacuna(string nombre, string lote, DateTime fecha_vencimiento, DateTime fecha_aplicacion) {
         
            Nombre = nombre;
            Lote = lote;
            Fecha_vencimiento = fecha_vencimiento;
            Fecha_aplicacion = fecha_aplicacion;
            
        }

        //Accesores
        public string Nombre { get => nombre; private set => nombre = value; }
        public string Lote { get => lote; private set => lote = value; }
        public DateTime Fecha_vencimiento { get => fecha_vencimiento; private set => fecha_vencimiento = value; }
        public DateTime Fecha_aplicacion { get => fecha_aplicacion; private set => fecha_aplicacion = value; }
    }
}
