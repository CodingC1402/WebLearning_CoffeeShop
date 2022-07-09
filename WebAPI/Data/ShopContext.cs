using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;

namespace WebAPI.Data
{
    public class ShopContext : DbContext
    {
        // Bad move to a key vault or something else later;
        public const string CONNECTION_STRING = "server=127.0.0.1;database=coffee_shop;user=root;password=password";

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Coffee> Coffees => Set<Coffee>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql(CONNECTION_STRING, ServerVersion.AutoDetect(CONNECTION_STRING))
                .UseValidationCheckConstraints()
                .UseEnumCheckConstraints();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(detail => new {detail.OrderId, detail.CoffeeId});
            
            modelBuilder.Entity<Coffee>().HasIndex(coffee => coffee.Name).IsUnique(true);
            modelBuilder.Entity<Employee>()
                .HasCheckConstraint("CK_Employee_StartResign", "`ResignDate` IS NULL OR `StartDate` < `ResignDate`")
                .HasCheckConstraint("CK_Employee_StartDOB", "`StartDate` > `DOB`");
        }
    }
}