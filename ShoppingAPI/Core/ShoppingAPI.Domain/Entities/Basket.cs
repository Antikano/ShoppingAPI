using ShoppingAPI.Domain.Common;
using ShoppingAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Domain.Entities
{
    public class Basket: BaseEntity
    {
        public AppUser User { get; set; }
        public int UserId { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
