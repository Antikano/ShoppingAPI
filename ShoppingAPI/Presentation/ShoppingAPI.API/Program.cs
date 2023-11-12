using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingAPI.Infrastructure;
using ShoppingAPI.Persistence;
using StackExchange.Redis;
using System.Text;

namespace ShoppingAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;

            //CORS
            builder.Services.AddCors
               (options => options
               .AddDefaultPolicy(policy => policy.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin()));

            // Add services to the container.
            builder.Services.AddPersistenceServices(configuration);
            builder.Services.AddInfrastructureServices();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("authScheme", options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidAudience = builder.Configuration["Token:Audience"],
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
                    };
                });

           //redis
            //var multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
            //builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}