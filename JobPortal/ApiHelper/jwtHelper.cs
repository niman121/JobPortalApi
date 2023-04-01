using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobPortal.ApiHelper
{
    public class jwtHelper
    {
        private readonly IConfiguration _configuration;

        public jwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(string userName, string role, string email)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.Role,role),
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenCred = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["JWt:Issuer"],
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenCred);
        }
    }
}
