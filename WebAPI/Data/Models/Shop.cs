using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Models
{
    public class Shop : Model
    {
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime EstablishedSince { get; set; } = DateTime.Now;

        public ICollection<Employee> Staffs { get; set; } = new List<Employee>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}