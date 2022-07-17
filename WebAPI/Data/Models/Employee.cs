using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Data.Models;

public class Employee : Person
{
    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime? ResignDate { get; set; }
    public int ShopId { get; set; }

    // Navigation Properties
    public Shop WorkPlace { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}