using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("[controller]")]
public class CoffeeController : ApiControllerBase<CoffeeService>
{
    public CoffeeController(CoffeeService service) : base(service)
    {}
}