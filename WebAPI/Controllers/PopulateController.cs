using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PopulateController : ControllerBase
    {
        public PopulateService Service { get; init; }
        public PopulateController(PopulateService service)
        {
            Service = service;
        }

        [HttpPost("customer")]
        public async Task<IActionResult> PostCustomer([FromQuery] int count) {
            try
            {
                await Service.PopulateCustomer(count);
                return Ok("Populate Successful");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete("customer")]
        public async Task<IActionResult> DeleteCustomer() {
            try {
                await Service.DepopulateCustomer();
                return Ok("Depopulated the customers");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
        
        [HttpPost("employee")]
        public async Task<IActionResult> PostEmployee([FromQuery] int count) {
            try
            {
                await Service.PopulateEmployees(count);
                return Ok("Populate Successful");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete("employee")]
        public async Task<IActionResult> DeleteEmployee() {
            try {
                await Service.DepopulateEmployee();
                return Ok("Depopulated the customers");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost("coffee")]
        public async Task<IActionResult> PostCoffee([FromQuery] int count) {
            try {
                await Service.PopulateCoffee(count);
                return Ok("Populate Successful");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete("coffee")]
        public async Task<IActionResult> DeleteCoffee() {
            try {
                await Service.DepopulateCoffee();
                return Ok("Depopulated the coffees");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost("order")]
        public async Task<IActionResult> PostOrder([FromQuery] int count) {
            try {
                await Service.PopulateOrder(count);
                return Ok("Populate Successful");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete("order")]
        public async Task<IActionResult> DeleteOrder() {
            try {
                await Service.DepopulateOrder();
                return Ok("Depopulated the orders");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
        
        [HttpPost("shop")]
        public async Task<IActionResult> PostShop([FromQuery] int count) {
            try {
                await Service.PopulateShop(count);
                return Ok("Populate Successful");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete("shop")]
        public async Task<IActionResult> DeleteShop() {
            try {
                await Service.DepopulateShop();
                return Ok("Depopulated the orders");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
    }
}