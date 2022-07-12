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
    public enum RoleType {
        Admin,
        Manager,
        Employee
    }

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; } = DateTime.Today;
    [Column(TypeName = "date")]
    public DateTime? ResignDate { get; set; }
    public int? ManagerId { get; set; }    public string Password { get; set; } = null!;
    public RoleType Role { get; set; } = RoleType.Employee;

    public string? RefreshToken { get; set; }
    // Navigation Properties
    public ICollection<Employee>? ManagingEmployees { get; set; }
    public Employee? Manager { get; set; }
}