using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Configs;
using WebAPI.Data.Models;

namespace WebAPI.Data
{
    public class ShopContext : DbContext
    {
        // Bad move to a key vault or something else later;
        public const string CONNECTION_STRING = "server=127.0.0.1;database=coffee_shop;user=root;password=password";

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Coffee> Coffees { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public DbSet<Shop> Shop { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql(CONNECTION_STRING, ServerVersion.AutoDetect(CONNECTION_STRING));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CoffeeConfig());
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new EmployeeConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfig());
            modelBuilder.ApplyConfiguration(new ShopConfig());

            //modelBuilder.Entity<Order>()
            //    .Property(x => x.Total)
            //    .ValueGeneratedOnAddOrUpdate()
            //    .HasComputedColumnSql(@"select sum(OrderDetails.Count * Coffees.Price) from `OrderDetails` inner join `Coffees` on OrderDetails.CoffeeId = Coffees.Id where OrderId = `Id`", true);
        }
    }
}