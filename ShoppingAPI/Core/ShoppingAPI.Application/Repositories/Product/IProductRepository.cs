using ShoppingAPI.Domain.DTOs;
using ShoppingAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Application.Repositories.Productt
{
    public interface IProductRepository : IRepository<Product>
    {
        public IQueryable<ProductWithCategoryNamesDTO> GetProductsWithCategory();

    }
}
