using Pampa.InSol.Entities;
using System.Security.Claims;

namespace Pampa.InSol.Infrastructure.Seguridad
{
    public interface IJwtSecurityTokenHandlerAdapter
    {
        JwtSecurityToken CreateToken(Usuario usuario);

        ClaimsPrincipal ValidateToken(string token);
    }
}