using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Models;

namespace WebAPI.Controllers
{
    public class ShopController : ApiControllerBase<Shop>
    {
        public override DbSet<Shop> DataSet => Context.Shop;
        public ShopController(ShopContext context) : base(context)
        {
        }
    }
}