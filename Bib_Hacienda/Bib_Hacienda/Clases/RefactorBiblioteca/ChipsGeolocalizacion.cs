using System;

namespace Bib_Hacienda.Clases.RefactorBiblioteca;

public class ChipsGeolocalizacion {

    private string Codigo;
    private double Latitud;
    private double Longitud;
    private DateTime FechaUltimaLectura;

    public ChipsGeolocalizacion (string codigo, double latitud, double longitud, DateTime fechaUltimaLectura) {
        
        Codigo = codigo;
        Latitud = latitud;
        Longitud = longitud;
        FechaUltimaLectura = fechaUltimaLectura;
        
    }
    
    public void ActualizarUbicacion(double latitud, double longitud) {
        
        Latitud = latitud;
        Longitud = longitud;
        FechaUltimaLectura = DateTime.Now;
        
    }

    public string ObtenerUbicacion() {
        
        return $"Latitud: {Latitud}, Longitud: {Longitud} (última lectura: {FechaUltimaLectura})";
        
    }
    
}