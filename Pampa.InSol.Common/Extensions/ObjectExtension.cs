namespace Pampa.InSol.Common.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Evalua si el objeto es nulo
        /// </summary>
        /// <param name="obj">Objecto</param>
        /// <returns>true si es nulo</returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Evalua si el obejto no es nulo
        /// </summary>
        /// <param name="obj">Objeto</param>
        /// <returns>true si no es nulo</returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }
    }
}
