using ShoppingAPI.Application.Repositories.Prodcut;
using ShoppingAPI.Domain.Entities;
using ShoppingAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Persistence.Repositories.EntityFramework.EfProduct
{
    public class EfProductRepository : EfEntityRepositoryBase<Product>, IProductRepository
    {
        public EfProductRepository(ShoppingAPIDbContext _context) : base(_context)
        {
        }
    }
}
