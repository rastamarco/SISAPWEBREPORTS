using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using sisapWebApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace sisapWebApi.Authorization
{
    public static class TokenService
    {
       
        public static string GenerateToken(User user)
        {
            var userToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Login", user.login.ToString()),
                    new Claim("TipoUsuario", user.tipoUsuario.ToString()),
                    new Claim("Filial", user.codFilial.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials (
                       new SymmetricSecurityKey(key), 
                       SecurityAlgorithms.HmacSha256Signature)
            };
            var token = userToken.CreateToken(tokenDescriptor);
            return userToken.WriteToken(token);
        }

    }
}
