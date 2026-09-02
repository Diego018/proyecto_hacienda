using System;
using System.Collections.Specialized;

namespace Bib_Hacienda.Clases.RefactorBiblioteca;

public abstract class Lacteo : ProductoGanadero {
    
    private DateTime FechaProduccion;
    private float Volumen;

    protected Lacteo(string id, string nombre, uint precio,
        DateTime fechaProduccion, float volumen) : base(id, nombre, precio) {
        
        FechaProduccion = fechaProduccion;
        Volumen = volumen;
        
    }
    
    public abstract uint CalcularPrecio();
    
    
    
}