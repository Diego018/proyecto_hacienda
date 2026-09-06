using System;
using Bib_Hacienda.Interfaces.Estrategias;

namespace Bib_Hacienda.Clases
{
    public class Venta
    {
        private Potrero potrero;
        private DateTime fecha;
        private Res res;
        private uint monto;
        private Usuario usuario;

        public Venta(Usuario usuario, Potrero potrero, DateTime fecha, Res res, uint monto)
        {
            Usuario = usuario;
            Potrero = potrero;
            Fecha = fecha;
            Res = res;
            Monto = monto;
        }

        //Accesores
        public Potrero Potrero { get => potrero; private set => potrero = value; }
        public DateTime Fecha { get => fecha; private set => fecha = value; }
        public Res Res { get => res; private set => res = value; }
        public uint Monto { get => monto; private set => monto = value; }
        public Usuario Usuario { get => usuario; private set => usuario = value; }

        // --- INICIO PATRÓN STRATEGY ---
        // Nuevo método: Delega el cálculo a la estrategia inyectada.
        // SRP: Venta gestiona la transacción, la Estrategia gestiona el precio.
        public void ProcesarVenta(IEstrategiaCalculoVenta estrategia)
        {
            if (estrategia == null) 
                throw new ArgumentNullException(nameof(estrategia), "La estrategia no puede ser nula.");
            
            if (this.Res == null) 
                throw new InvalidOperationException("No hay res asignada a la venta.");

            this.Monto = estrategia.CalcularMonto(this.Res);
            this.Fecha = DateTime.Now;
        }
    }
}