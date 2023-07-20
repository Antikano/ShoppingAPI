using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Application.DTOs;
using ShoppingAPI.Application.Repositories.Baskett;
using ShoppingAPI.Domain.Entities;
using ShoppingAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Persistence.Repositories.EntityFramework
{
    public class EfBasketRepository : EfEntityRepositoryBase<Basket>, IBasketRepository
    {
        readonly private ShoppingAPIDbContext context;
        public EfBasketRepository(ShoppingAPIDbContext _context) : base(_context)
        {
            context = _context;
        }

        public async Task<Basket> BasketWithProducts(int id)
        {
            var basket = await context.Baskets
                .Include(c => c.Products)
                .Where(p => p.Id == id)
                .Select(b => new Basket
                {
                    Id = b.Id,
                    Products = b.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImageData =p.ImageData,
                        Price =p.Price,
                        Stock = p.Stock
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return basket;
        }

        public async Task updateBasket(int id, UpdatedBasketDto basket)
        {
            var baskett = context.Baskets.Include(c => c.Products).FirstOrDefault(p => p.Id == id);

            baskett.Products = null;
            baskett.Products = new List<Product>();

            foreach (var item in basket.products)
            {
                var product = context.Products.FirstOrDefault(c => c.Id == item);
                if (product != null)
                {
                    baskett.Products.Add(product);
                }
            }

            context.SaveChanges();
        }

    }
}
