using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Data.Models;

namespace WebAPI.Security
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        readonly IConfiguration _configuration;
        public JwtTokenProvider(IConfiguration configuration) => _configuration = configuration;

        public (string accessToken, string refreshToken) GenerateToken(Employee employee)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var refreshSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:RefreshSecret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int refreshTokenValidityInDays);

            var principal = new Principal() {
                Id = employee.Id,
                Role = ((int)employee.Role)
            };
            var claims = principal.ToClaims();

            var refreshToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(refreshTokenValidityInDays),
                claims: claims,
                signingCredentials: new SigningCredentials(refreshSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            return (tokenHandler.WriteToken(token), tokenHandler.WriteToken(refreshToken));
        }

        public Principal GetPrincipal(string tokenStr) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(tokenStr, new() {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            }, out _);

            return Principal.Parse(claimsPrincipal.Claims);
        }
    }
}