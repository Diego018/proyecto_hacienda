using System;
using System.Collections.Generic;
using Bib_Hacienda.Clases;
using Bib_Hacienda.Eventos;
using Bib_Hacienda.Reglas;
using p_mvcHacienda.Infraestructura.puertos;
using p_mvcHacienda.Servicios.contratos;

namespace p_mvcHacienda.Servicios 
{
    public class VacunaService : IVacunaService 
    {
        private readonly IPersistenciaHacienda _haciendaPersistencia;

        public VacunaService(IPersistenciaHacienda persistencia) 
        {
            _haciendaPersistencia = persistencia ?? throw new ArgumentNullException(nameof(persistencia));
        }

        public string CrearVacuna(Vacuna vacuna) 
        {
            if (vacuna == null) 
            {
                throw new ArgumentNullException(nameof(vacuna));
            }

            return $"Vacuna '{vacuna.Nombre}' del lote '{vacuna.Lote}' creada exitosamente.";
        }

        public string AplicarVacuna(string potreroId, string nombreRes, Vacuna vacuna) 
        {
            if (vacuna == null)
            {
                throw new ArgumentNullException(nameof(vacuna));
            }

            try 
            {
                // 1. Cargar el agregado de la Hacienda
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

                // 2. Conectar el Observer Nativo (La regla de dominio escucha si la vacuna está vencida)
                vacuna.VacunaVencida += ReglaVacuna.OnVacunaVencida;

                // 3. Validar el estado de la vacuna usando la regla de negocio AS-IS
                if (DateTime.Now > vacuna.Fecha_vencimiento)
                {
                    // Dispara el evento Observer nativo
                    vacuna.OnVacunaVencida(new VacunaEventArgs(vacuna));
                    
                    throw new InvalidOperationException($"La vacuna '{vacuna.Nombre}' lote '{vacuna.Lote}' se encuentra vencida.");
                }

                // 4. Aplicar la vacuna a la res y persistir
                res.agregarVacuna(vacuna);
                _haciendaPersistencia.GuardarHacienda(hacienda);

                return $"Vacuna aplicada correctamente a la res {res.Nombre}.";
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error al aplicar la vacuna: {ex.Message}", ex);
            }
        }

        public List<Vacuna> ObtenerVacunasDisponibles() 
        {
            return new List<Vacuna>();
        }
    }
}