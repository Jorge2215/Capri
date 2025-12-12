using System;

namespace Pampa.InSol.Common
{
    public class ContextoUsuarioActual
    {
        private static Func<UsuarioActual> getCurrentUser;
        private static bool init;

        public static UsuarioActual Current
        {
            get
            {
                if (!init)
                {
                    throw new InvalidOperationException("Usuario no inicializado");
                }

                var user = getCurrentUser();
                return user;
            }
        }

        public static void Init(Func<UsuarioActual> getCurrentUser)
        {
            ContextoUsuarioActual.getCurrentUser = getCurrentUser;
            init = true;
        }
    }
}