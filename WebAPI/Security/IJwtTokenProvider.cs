using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Data.Models;

namespace WebAPI.Security
{
    public interface IJwtTokenProvider
    {
        public (string accessToken, string refreshToken) GenerateToken(Employee employee);
        public Principal GetPrincipal(string tokenStr);
    }
}