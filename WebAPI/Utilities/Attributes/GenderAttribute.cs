using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Utilities.Attributes;

public class GenderAttribute : ValidationAttribute
{
    private readonly string[] s_valid = new string[] {"Male", "Female"};

    public GenderAttribute(string errorMessage = "Invalid gender")
    {
        ErrorMessage = errorMessage;
    }
    public override bool IsValid(object? value) {
        string? gender = value as string;
        return gender == null || s_valid.Contains(gender);
    }
}