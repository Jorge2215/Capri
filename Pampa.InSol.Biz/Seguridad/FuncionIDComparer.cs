using Pampa.InSol.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pampa.InSol.Biz.Seguridad
{
    public class FuncionIDComparer : IEqualityComparer<Funcion>
    {
        #region IEqualityComparer<Funcion> Members

        /// <summary>
        /// Compara dos funciones y devuelve verdadero si son iguales.
        /// </summary>
        /// <param name="x">Funcion a comparar 1</param>
        /// <param name="y">Funcion a comparar 2</param>
        /// <returns>bool </returns>
        public bool Equals(Funcion x, Funcion y)
        {
            return x.Id == y.Id;
        }

        /// <summary>
        /// Devuelve el codigo hash para la función
        /// </summary>
        /// <param name="obj">Objeto instancia de la Funcion</param>
        /// <returns>Codigo Hash para la función</returns>
        public int GetHashCode(Funcion obj)
        {
            return obj.Id.GetHashCode();
        }

        #endregion
    }
}
