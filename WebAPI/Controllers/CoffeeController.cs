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

public class CoffeeController : ApiControllerBase<Coffee>
{
    public override DbSet<Coffee> DataSet => Context.Coffees;
    public CoffeeController(ShopContext context) : base(context)
    {}
}