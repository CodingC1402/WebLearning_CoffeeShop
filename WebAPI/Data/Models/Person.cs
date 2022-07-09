using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Utilities.Attributes;

namespace WebAPI.Data.Models
{
    public class Person : Model
    {
        [StringLength(50, MinimumLength = 1)]
        public string FullName { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime DOB  { get; set; }
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }
        [Gender]
        public string Gender { get; set; } = null!;
    }
}