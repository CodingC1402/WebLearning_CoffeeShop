using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("[controller]")]
public class CustomerController : ApiControllerBase<CustomerService>
{
    public CustomerController(CustomerService service) : base(service)
    {}
}