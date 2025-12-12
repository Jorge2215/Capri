using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;

namespace Pampa.InSol.Infrastructure.Seguridad
{
    public class JwtSecurityTokenHandlerFactory : IJwtSecurityTokenHandlerFactory
    {
        public JwtSecurityTokenHandler Create()
        {
            return new JwtSecurityTokenHandler();
        }
    }
}