using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data.Models;

public class Coffee : Model
{
    public string Name { get; set; } = null!;
    public string Notes { get; set; } = null!;
    public string Origin { get; set; } = null!;
    public string Variety { get; set; } = null!;
    public decimal Price { get; set; }

    // Navigation Properties
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}