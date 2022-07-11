using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Models;

public class Employee : Person
{
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; } = DateTime.Today;
    [Column(TypeName = "date")]
    public DateTime? ResignDate { get; set; }
    public int? ManagerId { get; set; }
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;

    // Navigation Properties
    public ICollection<Employee>? ManagingEmployees { get; set; }
    public Employee? Manager { get; set; }
}