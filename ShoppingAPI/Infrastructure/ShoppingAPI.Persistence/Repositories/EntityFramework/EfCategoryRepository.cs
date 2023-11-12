using ShoppingAPI.Application.Abstraction.Services;
using ShoppingAPI.Application.Repositories.Categoryy;
using ShoppingAPI.Domain.Entities;
using ShoppingAPI.Persistence.Contexts;

namespace ShoppingAPI.Persistence.Repositories.EntityFramework
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        //readonly private ICacheService _cacheService;
        readonly private ShoppingAPIDbContext context;
        public EfCategoryRepository(ShoppingAPIDbContext _context/*, ICacheService cacheService*/) : base(_context)
        {
            //_cacheService = cacheService;
            context = _context;
        }

        public List<Category> getCategoriesFromRedis()
        {
            //var categories = _cacheService.GetOrAdd<List<Category>>("Categories", () =>
            //{
                var dbCategories = context.Categories.ToList();
                //return dbCategories;
            //});

            if (dbCategories is null)
                throw new ArgumentNullException("categories", "Category list is null.");


            return dbCategories;

        }
    }
}
