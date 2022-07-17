using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Models;

namespace WebAPI.Services;

public class CoffeeService : Service<Coffee, ShopContext>
{
    protected override DbSet<Coffee> Data => Context.Coffees;
    public CoffeeService(ShopContext context) : base(context)
    {}

}