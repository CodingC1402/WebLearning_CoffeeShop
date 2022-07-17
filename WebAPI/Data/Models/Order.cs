using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data.Models;

public class Order : Model
{
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public int ShopId { get; set; }
    public decimal Total { get; set; }

    // Navigation Properties
    public Customer? Customer { get; set; }
    public Employee? Employee { get; set; }
    public Shop? Shop { get; set; }
    public ICollection<OrderDetail> Details { get; set; } = new List<OrderDetail>();

    public async Task<decimal> ComputeTotal(ShopContext context) {
        return Total = await context.OrderDetails
            .Where(x => x.OrderId == Id)
            .Include(x => x.Coffee)
            .SumAsync(x => x.Count * x.Coffee!.Price);
    }
}