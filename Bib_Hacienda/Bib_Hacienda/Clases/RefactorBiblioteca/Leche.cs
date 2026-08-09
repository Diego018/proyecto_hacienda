using System;

namespace Bib_Hacienda.Clases.RefactorBiblioteca;

public class Leche : Lacteo {
    
    private float PorcentajeGrasa;

    public Leche (string id, string nombre, uint precio, DateTime fechaProduccion, float volumen, float porcentajeGrasa)
        : base(id, nombre, precio, fechaProduccion, volumen) {
        
        PorcentajeGrasa = porcentajeGrasa;
        
    }
    
    public override uint CalcularPrecio() {
        
        throw new NotImplementedException("Pendiente por implementar");
        
    }
    
}