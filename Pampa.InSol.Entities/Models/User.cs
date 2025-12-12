using System;

namespace Pampa.InSol.Entities.Models
{
    /// <summary>
    /// Clase especifica de usuario, para que no quede ligado a ningún modelo de ORM y se más independiente de la cada aplicación.
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID del Usuario en la tabla de usuarios.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de usuario de Red/AD, también conocido como LLAVE o CHAVE.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Apellido del usuario.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Activo/Inactivo.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Fecha de creación del usuario en el sistema.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Ultima vez que se modificó el usuario.
        /// </summary>
        public DateTime LastUpdtae { get; set; }
    }
}
