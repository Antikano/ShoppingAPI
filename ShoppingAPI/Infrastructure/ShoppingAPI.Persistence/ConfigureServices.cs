using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingAPI.Application.Repositories.Prodcut;
using ShoppingAPI.Persistence.Contexts;
using ShoppingAPI.Persistence.Repositories.EntityFramework.EfProduct;
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


            services.AddScoped<IProductRepository, EfProductRepository>();

        }


    }
}
