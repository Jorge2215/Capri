using Pampa.InSol.Biz.Recursos;
using Pampa.InSol.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Pampa.InSol.Biz.Logica.Validacion
{
    /// <summary>
    /// Excepcion para validacion de negocio
    /// </summary>
    [Serializable]
    public class ValidacionNegocioException : ApplicationException
    {
        public ValidacionNegocioException(List<string> erroresValidacion) : base(ErrorMessages.ErrorValidacion)
        {
            this.ErroresValidacion = erroresValidacion;
        }

        public ValidacionNegocioException(string errorValidacion) : base(ErrorMessages.ErrorValidacion)
        {
            var errores = new List<string>() { errorValidacion };
            this.ErroresValidacion = errores;
        }

        public List<string> ErroresValidacion { get; private set; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info.IsNull())
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("ErroresValidacion", this.ErroresValidacion);
            base.GetObjectData(info, context);
        }

        public override string Message
        {
            get
            {
                if (this.ErroresValidacion.Count > 1)
                {
                    return string.Format("{0} - {1}",  base.Message, this.ErroresValidacion[0]) ;
                }

                return base.Message;
            }
        }
    }
}
