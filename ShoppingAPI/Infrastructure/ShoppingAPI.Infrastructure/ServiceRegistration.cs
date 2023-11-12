using Microsoft.Extensions.DependencyInjection;
using ShoppingAPI.Application.Abstraction.Services;
using ShoppingAPI.Application.Abstraction.Token;
using ShoppingAPI.Application.ViewModel.ClosedXML;
using ShoppingAPI.Infrastructure.Caching;
using ShoppingAPI.Infrastructure.Services.ExportToDoc;
using TokenHandler = ShoppingAPI.Infrastructure.Services.Token.TokenHandler;

namespace ShoppingAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<ICreWorksheet, CreWorksheet>();
            //services.AddScoped<ICacheService, RedisCacheService>();
        }
    }
}