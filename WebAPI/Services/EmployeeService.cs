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

    public EmployeeService(EmployeeRepo repository) : base(repository) {}

    public async Task<IEnumerable<Employee>> GetAllEmployee() {
        return await Repository.FindAll();
    }
    
    public async Task<Employee> GetEmployee(int id) {
        return await Repository.FindById(id);
    }

    public async Task<Employee?> AddEmployee(Employee employee) {
        var transaction = await Repository.StartTransaction();
        try
        {
            Repository.Add(employee);
            await Repository.Save();
            await transaction.CommitAsync();

            return employee;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return null;
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
        var transaction = await Repository.StartTransaction();
        try
        {
            if (truncate) await Repository.RemoveAllManagedEmployee(id);
            await Repository.AddManageEmployee(id, managedId);   
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }

    public async Task<Employee?> UpdateEmployee(int id, EmployeeDto updatedEmployee) {
        var transaction = await Repository.StartTransaction();
        try
        {
            var employee = await Repository.FindById(id);
            if (employee == null) throw new EmployeeNotFoundException();

            employee.AssignNotDefaultProperties(updatedEmployee);

            await Repository.Save();
            await transaction.CommitAsync();

            return employee;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return null;
        }
    }

    protected string EncryptPassword(string password) {
        return password;
    }
}