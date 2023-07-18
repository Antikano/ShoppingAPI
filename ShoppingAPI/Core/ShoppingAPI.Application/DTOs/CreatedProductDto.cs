using ShoppingAPI.Domain.Common;
using ShoppingAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Application.DTOs
{
    public class CreatedProductDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageData { get; set; }

        public string[] categoriesName { get; set; }

    }
}
