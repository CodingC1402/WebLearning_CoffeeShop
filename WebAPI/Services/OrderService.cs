using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Models;

namespace WebAPI.Services;

public class OrderService : Service<Order, ShopContext>
{
    protected override DbSet<Order> Data => Context.Orders;
    public OrderService(ShopContext context) : base(context)
    {
    }

}