using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Models;

namespace WebAPI.Services;

public class CustomerService : Service<Customer, ShopContext>
{
    protected override DbSet<Customer> Data => Context.Customers;
    public CustomerService(ShopContext context) : base(context)
    {}

}