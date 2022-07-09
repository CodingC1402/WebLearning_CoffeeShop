using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Utilities.Attributes;

public class RoleAttribute : ValidationAttribute
{
    private readonly string[] s_valid = new string[] {"Manager", "Employee"};

    public RoleAttribute(string errorMessage = "Invalid gender")
    {
        ErrorMessage = errorMessage;
    }
    public override bool IsValid(object? value) {
        string? role = value as string;
        return role != null && s_valid.Contains(role);
    }
}