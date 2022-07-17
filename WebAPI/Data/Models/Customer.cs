using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Models;

public class Customer : Person
{
    public DateTime RegisterSince { get; set; } = DateTime.Today;
    public uint Point { get; set; }

    // Navigation Properties
    public ICollection<Order>? Orders { get; set; }
}