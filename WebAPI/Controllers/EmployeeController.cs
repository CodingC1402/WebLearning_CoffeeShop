using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Models;
using WebAPI.DTOs.FrontEnd;
using WebAPI.Services;

namespace WebAPI.Controllers;

public class EmployeeController : ApiControllerBase<Employee>
{
    public override DbSet<Employee> DataSet => Context.Employees;
    
    public EmployeeController(ShopContext context) : base(context)
    {
    }

}