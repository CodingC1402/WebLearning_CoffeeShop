using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Models;
using WebAPI.DTOs.FrontEnd;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Services;

public class EmployeeService : Service<Employee, ShopContext>
{
    protected override DbSet<Employee> Data => Context.Employees;
    public EmployeeService(ShopContext context) : base(context)
    {}

}