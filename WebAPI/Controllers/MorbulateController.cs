using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class MorbulateController : ApiControllerBase<PopulateService>
    {
        public MorbulateController(PopulateService service) : base(service) 
        {}

        [HttpPost("customer")]
        public async Task<IActionResult> PostCustomer([FromQuery] int count) {
            try
            {
                await Service.PopulateCustomer(count);
                return Ok("Morbulate Successful");
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
                return Ok("Demorbulated the customers");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
        
        [HttpPost("employee")]
        public async Task<IActionResult> PostEmployee([FromQuery] int count) {
            try
            {
                await Service.PopulateEmployees(count);
                return Ok("Morbulate Successful");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete("employee")]
        public async Task<IActionResult> DeleteEmployee() {
            try {
                await Service.DepopulateEmployee();
                return Ok("Demorbulated the customers");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost("coffee")]
        public async Task<IActionResult> PostCoffee([FromQuery] int count) {
            try {
                await Service.PopulateCoffee(count);
                return Ok("Morbulate Successful");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete("coffee")]
        public async Task<IActionResult> DeleteCoffee() {
            try {
                await Service.DepopulateCoffee();
                return Ok("Demorbulated the coffees");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost("order")]
        public async Task<IActionResult> PostOrder([FromQuery] int count) {
            try {
                await Service.PopulateOrder(count);
                return Ok("Morbulate Successful");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete("order")]
        public async Task<IActionResult> DeleteOrder() {
            try {
                await Service.DepopulateOrder();
                return Ok("Demorbulated the orders");
            } catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
    }
}