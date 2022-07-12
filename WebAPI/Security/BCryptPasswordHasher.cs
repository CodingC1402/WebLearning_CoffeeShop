using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using WebAPI.Data.Models;

namespace WebAPI.Security
{
    public class BCryptPasswordHasher : IPasswordHasher<Employee>
    {
        public BCryptPasswordHasher(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; init; }

        public string HashPassword(Employee user, string password) 
            => user.Password = BCrypt.Net.BCrypt.HashPassword(password, Configuration["Salt"]);

        public PasswordVerificationResult VerifyHashedPassword(Employee user, string hashedPassword, string providedPassword) 
            => BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword) 
            ? PasswordVerificationResult.Success 
            : PasswordVerificationResult.Failed;
    }
}