using System.Collections.Generic;
using System.Linq;

namespace Pampa.InSol.Biz.Logica.Validacion
{
    public abstract class Validador<T>
    {
        public Validador()
        {
            this.Errores = new List<string>();
        }

        public bool EsValido
        {
            get
            {
                return !this.Errores.Any();
            }
        }

        private List<string> Errores { get; set; }

        /// <summary>
        /// Realiza las acciones de validacion
        /// </summary>
        /// <returns>Si hay errores lanza una excepcion del tipo ValidacionNegocioException</returns>
        public abstract void Validar(T modelo);

        /// <summary>
        /// Si encuentra errores en la lista de errores lanza una excepcion del tipo ValidacionNegocioException
        /// </summary>
        public void LanzarExcepcionDeValidacionSiCorresponde()
        {
            if (this.Errores.Count() > 0)
            {
                throw new ValidacionNegocioException(this.Errores);
            }
        }

        public void AgregarErrorDeValidacion(string error)
        {
            this.Errores.Add(error);
        }
    }
}