using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int CoffeeId { get; set; }
        [Range(1, 100)]
        public int Count { get; set; }

        // Navigation Properties
        public Order? Order { get; set; }
        public Coffee? Coffee { get; set; }
    }
}