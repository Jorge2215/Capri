using Microsoft.IdentityModel.Tokens;
using Pampa.InSol.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Pampa.InSol.Infrastructure.Seguridad
{
    public class JwtSecurityTokenHandlerAdapter : IJwtSecurityTokenHandlerAdapter
    {
        private readonly IJwtConfig jwtConfig;

        private readonly IJwtSecurityTokenHandlerFactory jwtSecurityTokenHandlerFactory;

        public JwtSecurityTokenHandlerAdapter(IJwtConfig jwtConfig, IJwtSecurityTokenHandlerFactory jwtSecurityTokenHandlerFactory)
        {
            this.jwtConfig = jwtConfig;
            this.jwtSecurityTokenHandlerFactory = jwtSecurityTokenHandlerFactory;
        }

        public JwtSecurityToken CreateToken(Usuario usuario)
        {
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddMinutes(this.jwtConfig.ExpirationMinutes),

                Issuer = this.jwtConfig.Issuer,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(this.jwtConfig.SecurityKey), this.jwtConfig.Algorithm),

                Subject = new ClaimsIdentity(new[]
                {
                    // Usuario
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString(), ClaimValueTypes.Integer64),
                    new Claim(ClaimTypes.Name, usuario.Nombre + " " + usuario.Apellido, ClaimValueTypes.String),
                    new Claim("username", usuario.UsuarioNT, ClaimValueTypes.String),
                })
            };
            if (usuario.Roles != null)
            {
                foreach (var rol in usuario.Roles)
                {
                    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, rol.Descripcion));
                }
            }

            JwtSecurityTokenHandler tokenHandler = this.jwtSecurityTokenHandlerFactory.Create();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtSecurityToken()
            {
                Access_token = tokenHandler.WriteToken(token),
                Expires_in = Convert.ToInt32(TimeSpan.FromMinutes(this.jwtConfig.ExpirationMinutes).TotalSeconds),
                Token_type = this.jwtConfig.TokenType
            };
        }

        public ClaimsPrincipal ValidateToken(String token)
        {
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(this.jwtConfig.SecurityKey),

                ValidateAudience = false,

                ValidIssuer = this.jwtConfig.Issuer
            };

            JwtSecurityTokenHandler tokenHandler = this.jwtSecurityTokenHandlerFactory.Create();

            try
            {
                SecurityToken securityToken;
                return tokenHandler.ValidateToken(token, validationParameters, out securityToken);
            }
            catch (SecurityTokenExpiredException)
            {
                // TODO: log?
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}