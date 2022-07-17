using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Models
{
    public class Shop : Model
    {
        public string Address { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime EstablishedSince { get; set; } = DateTime.Now;
        public int? ManagerId { get; set; }

        public Employee? Manager { get; set; }
        public ICollection<Employee> Staffs { get; set; } = new List<Employee>();
    }
}