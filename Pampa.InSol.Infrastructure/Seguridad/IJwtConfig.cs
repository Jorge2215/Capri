using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pampa.InSol.Infrastructure.Seguridad
{
    public interface IJwtConfig
    {
        string Algorithm { get; }

        int ExpirationMinutes { get; }

        string Issuer { get; }

        byte[] SecurityKey { get; }

        string TokenType { get; }
    }
}