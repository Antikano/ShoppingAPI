using Microsoft.EntityFrameworkCore;
using System;

namespace WebApplication1.Models
{
    public class DenemContext : DbContext
    {
        public DenemContext(DbContextOptions<DenemContext> options)
        : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=denemDb;Trusted_Connection=True;");
            }
        }
    }
}
