using System;

namespace Bib_Hacienda.Clases.Validaciones
{
    public abstract class ValidadorHandler
    {
        protected ValidadorHandler _siguiente;

        public ValidadorHandler SetNext(ValidadorHandler handler)
        {
            _siguiente = handler;
            return handler;
        }

        public virtual bool Validar(object entidad)
        {
            if (_siguiente != null)
            {
                return _siguiente.Validar(entidad);
            }
            return true;
        }
    }
}