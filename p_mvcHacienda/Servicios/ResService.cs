using System;
using System.Collections.Generic;
using Bib_Hacienda.Clases;
using Bib_Hacienda.Eventos;
using Bib_Hacienda.Reglas;
using p_mvcHacienda.Infraestructura.puertos;
using p_mvcHacienda.Servicios.Contratos;

namespace p_mvcHacienda.Servicios 
{
    public class ResService : IResService 
    {
        private readonly IPersistenciaHacienda _haciendaPersistencia;

        public ResService(IPersistenciaHacienda persistencia) 
        {
            _haciendaPersistencia = persistencia ?? throw new ArgumentNullException(nameof(persistencia));
        }

        public string AlimentarRes(string potreroId, string nombreRes, uint cantidad) 
        {
            try 
            {
                Hacienda hacienda = _haciendaPersistencia.CargarHacienda();
                Potrero potrero = hacienda.buscar_potrero(potreroId);

                if (potrero == null) 
                {
                    throw new InvalidOperationException($"No se encontró el potrero '{potreroId}'");
                }

                Res res = potrero.buscar_res(nombreRes);

                if (res == null) 
                {
                    throw new InvalidOperationException($"No se encontró la res '{nombreRes}' en el potrero '{potreroId}'");
                }

                // 1. Suscripción de observadores estáticos de la Regla de Dominio
                res.PesoMinimoAlcanzado += ReglaRes.OnPesoMinimoAlcanzado;
                res.AptaParaVenta += ReglaRes.OnAptaParaVenta;

                // 2. Modificación de estado (Alimentar)
                res.alimentar(cantidad);

                // 3. Evaluación de eventos de Dominio
                if (res.EstaEnPesoMinimo())
                {
                    res.OnPesoMinimoAlcanzado(new ResEventArgs(res));
                }

                if (res.EstaAptaParaVenta())
                {
                    res.OnAptaParaVenta(new ResEventArgs(res));
                }

                _haciendaPersistencia.GuardarHacienda(hacienda);

                return $"La res '{res.Nombre}' ha sido alimentada, ahora pesa {res.Peso} kg.";
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error al alimentar la res: {ex.Message}", ex);
            }
        }

        public Res BuscarRes(string potreroId, string nombreRes) 
        {
            Hacienda hacienda = _haciendaPersistencia.CargarHacienda();
            Potrero potrero = hacienda.buscar_potrero(potreroId);

            return potrero?.buscar_res(nombreRes);
        }

        public List<(Potrero Potrero, Res Res)> ObtenerTodasLasReses() 
        {
            Hacienda hacienda = _haciendaPersistencia.CargarHacienda();
            var resultado = new List<(Potrero, Res)>();

            foreach (var potrero in hacienda.obtener_potreros()) 
            {
                foreach (var res in potrero.obtener_reses()) 
                {
                    resultado.Add((potrero, res));
                }
            }

            return resultado;
        }
    }
}