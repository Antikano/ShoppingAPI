using ShoppingAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Application.DTOs
{
    public class UpdatedBasketDto
    {
        public int id { get; set; }
        public ICollection<int> products { get; set; }

    }
}
