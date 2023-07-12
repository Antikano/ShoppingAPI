using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShoppingAPI.Application.Abstraction.Token;
using ShoppingAPI.Infrastructure.Services.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler = ShoppingAPI.Infrastructure.Services.Token.TokenHandler;

namespace ShoppingAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}