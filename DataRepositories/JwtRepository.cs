using JwtAuthExample.IRepositories;
using JwtAuthExample.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthExample.DataRepositories
{
    public class JwtRepository : IJwtRepository
    {
        private readonly IConfiguration configuration;
       public JwtRepository(IConfiguration configuration) { 
        this.configuration = configuration; 
        }

        public string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtKey")));
            var issuer = configuration.GetValue<string>("JwtIssuer");
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            
            };
            var token = new JwtSecurityToken(issuer,
            issuer,
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
