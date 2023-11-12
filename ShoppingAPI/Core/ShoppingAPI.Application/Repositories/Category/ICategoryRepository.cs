using ShoppingAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Application.Repositories.Categoryy
{
    public interface ICategoryRepository: IRepository<Category>
    {
        public List<Category> getCategoriesFromRedis();
    }
}
