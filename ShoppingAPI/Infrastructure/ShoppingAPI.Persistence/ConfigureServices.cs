using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShoppingAPI.Application.Repositories.Baskett;
using ShoppingAPI.Application.Repositories.Categoryy;
using ShoppingAPI.Application.Repositories.Productt;
using ShoppingAPI.Domain.Entities.Identity;
using ShoppingAPI.Persistence.Contexts;
using ShoppingAPI.Persistence.Repositories.EntityFramework;

namespace ShoppingAPI.Persistence
{
    public static class ConfigureServices
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShoppingAPIDbContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("DbContextCS")));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ShoppingAPIDbContext>();

            services.AddScoped<IProductRepository, EfProductRepository>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            services.AddScoped<IBasketRepository, EfBasketRepository>();

        }
    }
}
