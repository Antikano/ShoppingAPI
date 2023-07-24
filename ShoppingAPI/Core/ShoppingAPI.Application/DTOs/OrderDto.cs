using ShoppingAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Application.DTOs
{
    public class OrderDto
    {
        public string[] productNames { get; set; }
        public int basketId { get; set; }
        public DateTime createdDate { get; set; }
        public int totalPrice { get; set; }
    }
}
