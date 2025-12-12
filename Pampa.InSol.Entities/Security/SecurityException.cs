using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Security
{
    public class InvalidUserException : SecurityException
    {
        public InvalidUserException(string error, string userError = null, Exception inner = null)
            : base(error, userError, inner)
        {

        }
    }

    public class UserHasNotPermissionException : SecurityException
    {
        public UserHasNotPermissionException(string error, string userError = null, Exception inner = null)
            : base(error, userError, inner)
        {

        }
    }

    public class OnlyInternalException : SecurityException
    {
        public OnlyInternalException(string error, string userError = null, Exception inner = null)
            : base(error, userError, inner)
        {

        }
    }

    public class SecurityException : Exception
    {
        public string error { get; set; }
        public string userError { get; set; }
        public SecurityException(string error, string userError = null, Exception ex = null)
            : base(error, ex)
        {
            this.error = error;
            this.userError = userError;
        }
    }
}
