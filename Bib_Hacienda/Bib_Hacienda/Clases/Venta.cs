using System;

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
    }
}