using System;

namespace Pampa.InSol.Common.Utils
{
    public static class BswFraction
    {
        public static string ToBswFraction(this double d)
        {
            double integer = Math.Truncate(d);
            double dec = d - integer;

            if (dec != 0.0D)
            {
                return integer.ToString() + " " + (new Fraction(dec)).ToString() + "\"";
            }
            else
            {
                return integer.ToString() + "\"";
            }
        }
    }
}
