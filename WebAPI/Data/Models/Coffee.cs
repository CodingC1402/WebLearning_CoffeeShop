using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data.Models;

public class Coffee : Model
{
    [StringLength(30)]
    public string Name { get; set; } = null!;
    [StringLength(100)]
    public string Notes { get; set; } = null!;
    [StringLength(100)]
    public string Origin { get; set; } = null!;
    [StringLength(30)]
    public string Variety { get; set; } = null!;
    [Precision(4, 2)]
    public decimal Price { get; set; }


    // Navigation Properties
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}