using System;

namespace Pampa.InSol.Biz.Contratos.Seguridad
{
    public interface IAuthorizableController
    {
        /// <summary>
        /// Método para resolver si el usuario que está ejecutando la acción actual tiene o permisos para realizar la función provista.
        /// El finalidad de implementar esta interfaz es que los resultados sean cacheados para minimizar los accesos a base de datos en vistas donde se requieran mostrar/ocultar muchas elementos(acciones), como un menú.
        /// </summary>
        /// <param name="functionId">ID de la función</param>
        /// <returns>TRUE: sí el usuario tiene permisos, FALSE: el resto de los casos.</returns>
        bool IsUserAuthorized(int functionId);
    }
}
