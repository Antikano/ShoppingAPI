using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Domain.Common;
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

        public DbSet<Category> Categories { get; set; }


        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow
                };

            }

            return await base.SaveChangesAsync(cancellationToken);
        }


    }
}
