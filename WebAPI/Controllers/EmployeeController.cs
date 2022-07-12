using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebAPI.Data.Models;
using WebAPI.DTOs.FrontEnd;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("[controller]")]
public class EmployeeController : ApiControllerBase<EmployeeService>
{
    public EmployeeController(EmployeeService service) : base(service)
    {}

    [HttpGet("{id:int}")]
    [Authorize]
    [ProducesErrorResponseType(typeof(NotFoundResult))]
    public async Task<IActionResult> GetEmployees([FromRoute] int id) {
        return Ok(await Service.GetEmployee(id));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddEmployee([FromBody] Employee employee) {
        await Service.AddEmployee(employee);
        return Ok(employee);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] EmployeeDto employee) {
        try {
            await Service.UpdateEmployee(id, employee);
            return Ok(employee);
        } catch (InvalidOperationException) {
            return NotFound("Can't update employee with id " + id);
        }
    }
}