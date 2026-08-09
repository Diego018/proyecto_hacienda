using System;
using System.Collections.Generic;

namespace Bib_Hacienda.Clases;

public abstract class ProductoGanadero {
    
    protected string Id;
    protected string Nombre;
    protected uint Precio;

    protected ProductoGanadero (string id, string nombre, uint precio) {
        
        Id = id;
        Nombre = nombre;
        Precio = precio;
        
    }

    public uint ObtenerPrecio() {
        
        return Precio;

    }
    
}