using System;

namespace Pampa.InSol.Common.Extensions
{
    public static class DatetimeExtension
    {
        /// <summary>
        /// Devuelve el primer dia del mes para la fecha dada
        /// </summary>
        public static DateTime PrimerDiaDelMes(this DateTime fecha)
        {
            return new DateTime(fecha.Year, fecha.Month, 1);
        }
    }
}
