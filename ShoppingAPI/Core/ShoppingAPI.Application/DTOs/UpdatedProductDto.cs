using ShoppingAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Application.DTOs
{
    public class UpdatedProductDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string imageData { get; set; }
        //public ICollection<Category> Categories { get; set; }

    }
}
