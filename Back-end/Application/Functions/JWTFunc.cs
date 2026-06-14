using Domain.Classes;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Application.Functions
{
    public class JWTFunc
    {
        public string CreateJwt(User user)
        {
            var claim = new List<Claim>
            {
                new Claim("Login", user.Login),
                new Claim("Id", user.Id.ToString())
            };

            var jwt = new JwtSecurityToken
            (
                expires: DateTime.UtcNow.Add(SParams.TIME_SPAN),
                claims: claim,
                signingCredentials: new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SParams.JWT_K)),
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
