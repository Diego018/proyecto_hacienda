using System;
using System.Collections.Generic;
using Bib_Hacienda.Clases.RefactorBiblioteca;

namespace Bib_Hacienda.Clases
{
    public abstract class Res
    {
        private string nombre;
        private uint peso;
        private DateTime fechaNacimiento;
        private List<Vacuna> vacunasAplicadas;
        private ChipsGeolocalizacion chip;
        private HistoriaClinica historiaClinica;
        private List<ProductoGanadero> productos;
        
        public string Nombre => nombre;
        public uint Peso => peso;
        public DateTime FechaNacimiento => fechaNacimiento;
        public List<Vacuna> VacunasAplicadas => vacunasAplicadas;

        protected Res(string nombre, uint peso, DateTime fechaNacimiento) {
            
            this.nombre = nombre;
            this.peso = peso;
            this.fechaNacimiento = fechaNacimiento;
            vacunasAplicadas = new List<Vacuna>();
            productos = new List<ProductoGanadero>();
            
        }

        public void alimentar(uint cantidad) {
            
            peso += cantidad;
            
        }

        public void agregarVacuna(Vacuna vacuna) {
            
            vacunasAplicadas.Add(vacuna);
        }

        // todo: calcula la edad en meses a partir de la fecha de nacimiento
        public ushort Edad() {
            
            int meses = ((DateTime.Now.Year - fechaNacimiento.Year) * 12)
                + DateTime.Now.Month - fechaNacimiento.Month;

            if (DateTime.Now.Day < fechaNacimiento.Day) {
                
                meses--;
                
            }

            return (ushort)Math.Max(meses, 0);
            
        }
        
        public abstract bool ValidarCrecimiento(); 
        
        //metodos para manejar los eventos
        
        public abstract bool EstaEnPesoMinimo();
        public abstract bool EstaAptaParaVenta();
        
    }
    
}