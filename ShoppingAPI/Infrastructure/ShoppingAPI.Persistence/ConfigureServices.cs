using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingAPI.Application.Repositories.Baskett;
using ShoppingAPI.Application.Repositories.Categoryy;
using ShoppingAPI.Application.Repositories.Productt;
using ShoppingAPI.Domain.Entities.Identity;
using ShoppingAPI.Persistence.Contexts;
using ShoppingAPI.Persistence.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Persistence
{
    public static class ConfigureServices
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ShoppingAPIDbContext>(option =>
                option.UseSqlServer
                (@"Server=DESKTOP-CQ6T5PI;Database=ShoppingApiDB;
                Trusted_Connection=True;Encrypt=False;")); // connectionstring 'i düzelt !

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
