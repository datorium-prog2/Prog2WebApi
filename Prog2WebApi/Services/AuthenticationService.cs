using Prog2WebApi.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Prog2WebApi.Services
{
    public class AuthenticationService
    {
        public static string CreateJwtToken(User user)
        {
            // aizpildam informāciju par lietotāju - claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // izveidojam atslēgu, ar kuru šifrēsim tokenu
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("PROG2_LIELS_NOSLEPUMS_PROG2_LIELS_NOSLEPUMS")
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // norādam visus tokena atribūtus
            var token = new JwtSecurityToken(
                issuer: "Prog2WebApi",
                audience: "Prog2WebApi",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
