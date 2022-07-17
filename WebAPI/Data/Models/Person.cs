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
        public enum GenderType {
            Male,
            Female,
            Non
        }

        public string FullName { get; set; } = null!;
        public DateTime DOB  { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public GenderType Gender { get; set; }
    }
}