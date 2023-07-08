using ShoppingAPI.Application.Repositories.Categoryy;
using ShoppingAPI.Application.Repositories.Productt;
using ShoppingAPI.Domain.Entities;
using ShoppingAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Persistence.Repositories.EntityFramework
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(ShoppingAPIDbContext _context) : base(_context)
        {
        }
    }
}
