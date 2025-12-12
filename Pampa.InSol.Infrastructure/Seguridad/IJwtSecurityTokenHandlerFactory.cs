using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Infrastructure.Seguridad
{
    public interface IJwtSecurityTokenHandlerFactory
    {
        JwtSecurityTokenHandler Create();
    }
}
