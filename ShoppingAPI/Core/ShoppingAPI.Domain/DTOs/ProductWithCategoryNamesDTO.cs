using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Domain.DTOs
{
    public class ProductWithCategoryNamesDTO
    {
        
            public int Id { get; set; }
            public string Name { get; set; }
            public int Stock { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }

            public string ImageData { get; set; }
            public List<string> CategoryNames { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime UpdatedDate { get; set; }
        

    }

}
