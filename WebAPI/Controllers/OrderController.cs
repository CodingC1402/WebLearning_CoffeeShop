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

public class OrderController : ApiControllerBase<Order>
{
    public override DbSet<Order> DataSet => Context.Orders;
    
    public OrderController(ShopContext context) : base(context)
    {   
    }
}