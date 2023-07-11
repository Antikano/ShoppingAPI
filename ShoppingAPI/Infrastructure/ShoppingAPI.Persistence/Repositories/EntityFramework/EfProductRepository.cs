using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Application.Repositories.Categoryy;
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
                    ImageData= p.ImageData,
                    Description = p.Description,
                    CategoryNames = p.Categories.Select(c => c.Name).ToList(),
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate
                });

            return products;
        }


        public void AddProductWithCategories(CreatedProductDto p)
        {



            var product = new Product()
            {
                Name = p.Name,
                Description = p.Description,
                Stock = p.Stock,
                Price = p.Price,
                ImageData = p.ImageData,
                Categories = new List<Category>() { }
            };


            foreach (var item in p.categoriesName)
            {
                var canc = context.Categories.FirstOrDefault(c => c.Name == item);

                if(canc is not null)
                product.Categories.Add(canc);


            }

            context.Add(product);
            context.SaveChanges();



        }


    }
}
