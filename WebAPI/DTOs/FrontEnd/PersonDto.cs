using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Utilities.Attributes;

namespace WebAPI.DTOs.FrontEnd
{
    public class PersonDto
    {
        [StringLength(50, MinimumLength = 1)]
        public string? FullName { get; set; } = null!;
        public DateTime? DOB  { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }
        [Gender]
        public string? Gender { get; set; } = null!;
    }
}