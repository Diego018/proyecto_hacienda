using System;

namespace Bib_Hacienda.Clases.RefactorBiblioteca;

public class Piel : ProductoGanadero {

    private float Area;
    private string Calidad;

    protected Piel(string id, string nombre, uint precio, float area, string calidad) : base(id, nombre, precio) {
        
        Area = area;
        Calidad = calidad;
        
    }

    public uint CalcularPrecio() {

        throw new NotImplementedException("Pendiente de implementar");
        
    }

}