using ShoppingAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Domain.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } 
        public DateTime CreatedDate { get; set; }
    }
}
