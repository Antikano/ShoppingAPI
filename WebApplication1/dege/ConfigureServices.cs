using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dege
{
    public static class ConfigureServices
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<DenemContext>(option =>
                option.UseSqlServer(@"Server=DESKTOP-CQ6T5PI;Database=denekekDb;
                Trusted_Connection=True;Encrypt=False;"));


           

        }


    }
}
