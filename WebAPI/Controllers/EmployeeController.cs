using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    [HttpGet]
    public async Task<IActionResult> GetEmployees([FromQuery] int? id) {
        if (id == null) {
            return Ok(await Service.GetAllEmployee());
        } else {
            return Ok(await Service.GetEmployee(id.Value));
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] Employee employee) {
        return Ok(await Service.AddEmployee(employee));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmployee([FromQuery] int id, [FromBody] EmployeeDto employee) {
        return Ok(await Service.UpdateEmployee(id, employee));
    }

    [HttpPut("manager")]
    public async Task<IActionResult> SetManager([FromQuery] int id, [FromQuery] int managerId) {
        await Service.SetManager(id, managerId);
        return Ok();
    }

    [HttpPut("managed")]
    public async Task<IActionResult> SetManaged([FromQuery] int id, [FromBody] int[] managedId, [FromQuery] bool truncated = false) {
        await Service.SetManagedEmployee(id, managedId, truncated);
        return Ok();
    }
}