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
    [Precision(4, 2)]
    public decimal Total { get; set; }

    // Navigation Properties
    public Customer? Customer { get; set; }
    public Employee? Employee { get; set; }
    public ICollection<OrderDetail>? Details { get; set; }
}