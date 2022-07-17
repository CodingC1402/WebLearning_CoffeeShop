using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Models;

namespace WebAPI.Services
{
    public class ShopService : Service<Shop, ShopContext>
    {
        protected override DbSet<Shop> Data => Context.Shop;
        public ShopService(ShopContext context) : base(context)
        {}
    }
}