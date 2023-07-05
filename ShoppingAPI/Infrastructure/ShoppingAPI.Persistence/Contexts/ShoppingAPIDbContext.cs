using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Persistence.Contexts
{
    public class ShoppingAPIDbContext : DbContext
    {
        public ShoppingAPIDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        
    }
}
