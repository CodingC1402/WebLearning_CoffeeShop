using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebAPI.Data.Models;

namespace WebAPI.DTOs.FrontEnd
{
    public class EmployeeDto : PersonDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? ResignDate { get; set; }
        public int? ManagerId { get; set; }
    }
}