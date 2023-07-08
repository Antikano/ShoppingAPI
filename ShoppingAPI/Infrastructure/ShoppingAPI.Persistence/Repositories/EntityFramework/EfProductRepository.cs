using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Application.Repositories.Productt;
using ShoppingAPI.Domain.DTOs;
using ShoppingAPI.Domain.Entities;
using ShoppingAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Persistence.Repositories.EntityFramework
{
    public class EfProductRepository : EfEntityRepositoryBase<Product>, IProductRepository
    {
        readonly private ShoppingAPIDbContext context;
        public EfProductRepository(ShoppingAPIDbContext _context) : base(_context)
        {
           context= _context;
        }

        public IQueryable<ProductWithCategoryNamesDTO> GetProductsWithCategory()
        {
            var products = context.Products
                .Select(p => new ProductWithCategoryNamesDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    Price = p.Price,
                    Description = p.Description,
                    CategoryNames = p.Categories.Select(c => c.Name).ToList(),
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate
                });

            return products;
        }
    }
}
