using System;

namespace Bib_Hacienda.Clases.RefactorBiblioteca;

public class Queso : Lacteo {
    
    private string Tipo;
    private uint Peso;

    public Queso(string id, string nombre, uint precio, DateTime fechaProduccion, float volumen, string tipo, uint peso)
        : base(id, nombre, precio, fechaProduccion, volumen) {
        
        Tipo = tipo;
        Peso = peso;
        
    }
    
    public override uint CalcularPrecio() {
       
        throw new NotImplementedException("Pendiente de implementar");
        
    }
    
    
}