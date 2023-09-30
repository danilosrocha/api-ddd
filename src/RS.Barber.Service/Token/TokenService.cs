using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RS.Barber.Domain.Entities;
using RS.Barber.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RS.Barber.Application.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario user)
        {
            var issuer = _configuration["JwtSettings:Issuer"]; 
            var audience = _configuration["JwtSettings:Audience"];

            var claims = new List<Claim>
            {
                new Claim("cid", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Tipo.ToString())
            };

            var expires = DateTime.Now.AddMinutes(120);
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(expires: expires, signingCredentials: credentials, claims: claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

    }
}
