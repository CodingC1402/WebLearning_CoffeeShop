using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

public class CustomerController : ApiControllerBase<Customer>
{
    public override DbSet<Customer> DataSet => Context.Customers;
    
    public CustomerController(ShopContext context) : base(context)
    {
    }
}