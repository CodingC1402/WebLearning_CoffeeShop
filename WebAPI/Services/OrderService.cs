using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Models;
using WebAPI.Data.Repos;

namespace WebAPI.Services;

public class OrderService : Service<OrderRepo, Order>
{
    public OrderService(OrderRepo repository) : base(repository)
    {
    }
}