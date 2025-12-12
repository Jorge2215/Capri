using System;
using Microsoft.IdentityModel.Tokens;

namespace Pampa.InSol.Infrastructure.Seguridad
{
    public class JwtConfig : IJwtConfig
    {
        public String Algorithm => SecurityAlgorithms.HmacSha256Signature;

        public Int32 ExpirationMinutes => 720;

        public String Issuer => "pec";

        public Byte[] SecurityKey => Convert.FromBase64String("UGVjQXBwU2VjdXJpdHlLZXk=");  // PecAppSecurityKey

        public String TokenType => "Bearer";
    }
}