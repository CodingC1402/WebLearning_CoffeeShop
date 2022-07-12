using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebAPI.Data.Models;
using WebAPI.Data.Repos;
using WebAPI.DTOs.FrontEnd;
using WebAPI.Security;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Services;

public class EmployeeService : Service<EmployeeRepo, Employee>
{
    public class EmployeeNotFoundException : Exception {
        public EmployeeNotFoundException(string message = "Employee not found") : base(message) { }
        public EmployeeNotFoundException(int id) : this($"Employee with id {id} not found") { }
    }
    public class LoginFailedException : Exception {
        public LoginFailedException() : base("Employee id or password is incorrect") { }
    }
    public class RefreshTokenException : Exception { 
        public RefreshTokenException() : base("Refresh token is invalid, please login again") { }
    }

    private readonly IPasswordHasher<Employee> _passwordHasher;
    private readonly IJwtTokenProvider _tokenProvider;

    public EmployeeService(
        EmployeeRepo repository, 
        IPasswordHasher<Employee> passwordHasher, 
        IJwtTokenProvider tokenProvider) : base(repository)
        => (_passwordHasher, _tokenProvider) = (passwordHasher, tokenProvider);

    public async Task<IEnumerable<Employee>> GetAllEmployee() {
        return await Repository.FindAll();
    }
    
    public async Task<Employee> GetEmployee(int id) {
        return await Repository.FindById(id);
    }

    public async Task<LoginReplyDto> Login(int id, string password) {
        try {
            var employee = await Repository.FindById(id);
            var result = _passwordHasher.VerifyHashedPassword(employee, employee.Password, password);
    
            var reply = new LoginReplyDto();
            switch (result)
            {
                case PasswordVerificationResult.Failed:
                    throw new Exception();
                case PasswordVerificationResult.SuccessRehashNeeded:
                case PasswordVerificationResult.Success:
                    (reply.AccessToken, reply.RefreshToken) = _tokenProvider.GenerateToken(employee);
                    employee.RefreshToken = reply.RefreshToken;
                    await Repository.Save();
                    
                    reply.AssignNotDefaultProperties(employee);
                    break;
            }

            return reply;
        } catch (Exception) {
            throw new LoginFailedException();
        }
    }
    public async Task Logout(int id) {
        var employee = await Repository.FindById(id);
        if (employee == null) return;

        employee.RefreshToken = null;
    }
    public async Task<RefreshReplyDto> RefreshToken(string refreshToken) {
        try {
            var principal = _tokenProvider.GetPrincipal(refreshToken);
            var employee = await Repository.FindById(principal.Id);
            if (employee.RefreshToken != refreshToken) {
                throw new Exception();
            }

            var refreshReplyDto = new RefreshReplyDto();
            (refreshReplyDto.AccessToken, refreshReplyDto.RefreshToken) = _tokenProvider.GenerateToken(employee);
            employee.RefreshToken = refreshReplyDto.RefreshToken;

            await Repository.Save();

            return refreshReplyDto;
        } catch (Exception) {
            throw new RefreshTokenException();
        }
    }
    public async Task<Employee?> ResignEmployee(int id) {
        var employee = await Repository.FindById(id);
        if (employee == null) return null;

        employee.ResignDate = DateTime.Today;
        await Repository.Save();

        return employee;
    }
    public async Task<Employee?> ClearResignEmployee(int id) {
        var employee = await Repository.FindById(id);
        if (employee == null) return null;

        employee.ResignDate = null;
        await Repository.Save();

        return employee;
    }
    public async Task SetManagedEmployee(int id, int[] managedId, bool truncate) {
        if (truncate) await Repository.RemoveAllManagedEmployee(id);
        await Repository.AddManageEmployee(id, managedId);   
    }
}