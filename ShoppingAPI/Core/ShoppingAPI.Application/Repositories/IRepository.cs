using ShoppingAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Application.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> data);
        void Update(T entity);
        bool Remove(T entity);
        bool Remove(int id);

    }
}
