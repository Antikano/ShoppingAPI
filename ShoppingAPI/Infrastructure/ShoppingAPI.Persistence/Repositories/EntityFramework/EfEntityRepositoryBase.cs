using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Application.Repositories;
using ShoppingAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Persistence.Repositories.EntityFramework
{
    public class EfEntityRepositoryBase<T> : IRepository<T> 
        where T : class, new()
       
    {
        readonly private ShoppingAPIDbContext context;
        public EfEntityRepositoryBase(ShoppingAPIDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> AddAsync(T entity)
        {
            
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
                return true;

            
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            
                var addedEntities = entities.Select(entity =>
                {
                    var addedEntity = context.Entry(entity);
                    addedEntity.State = EntityState.Added;
                    return addedEntity.Entity;
                }).ToList();

                context.AddRange(addedEntities);
                await context.SaveChangesAsync();
                return true;
            
        }


        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
           
                IQueryable<T> query = context.Set<T>();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return query;
            
        }


        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            
                IQueryable<T> query = context.Set<T>().AsQueryable();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return await query.FirstOrDefaultAsync();
            
        }


        public bool Remove(T entity)
        {
            
                var entry = context.Entry(entity);

                if (entry.State == EntityState.Detached)
                {
                    context.Attach(entity);
                }

                context.Remove(entity);
                return context.SaveChanges() > 0;
            
        }

        public bool Remove(int id)
        {
            
                var entity = context.Set<T>().Find(id);

                if (entity == null)
                {
                    return false;
                }

                context.Remove(entity);
                return context.SaveChanges() > 0;
            
        }


        public void Update(T entity)
        {
            
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            
        }

    }
}
