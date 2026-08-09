using System;
using System.Security.AccessControl;
using Bib_Hacienda.Clases;

namespace DefaultNamespace;

public class Carne : ProductoGanadero {

    private uint Peso;
    private string tipoCorte;

    protected Carne (string id, string nombre, uint precio, uint peso, string tipoCorte) : base (id, nombre, precio) {
        
        this.Peso = peso;
        this.tipoCorte = tipoCorte;

    }

    public uint CalcularPrecio() {

        throw new NotImplementedException("Pendiente de implementar");

    }


}