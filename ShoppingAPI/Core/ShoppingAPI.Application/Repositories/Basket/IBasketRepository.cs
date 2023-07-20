using ShoppingAPI.Application.DTOs;
using ShoppingAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Application.Repositories.Baskett
{
    public interface IBasketRepository:IRepository<Basket>
    {
        public  Task<Basket> BasketWithProducts(int id);

        public Task updateBasket(int id, UpdatedBasketDto basket);
    }
}
