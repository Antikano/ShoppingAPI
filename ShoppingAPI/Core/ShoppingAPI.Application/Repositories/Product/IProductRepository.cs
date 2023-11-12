using ShoppingAPI.Application.DTOs;
using ShoppingAPI.Domain.DTOs;
using ShoppingAPI.Domain.Entities;
using System.Linq.Expressions;

namespace ShoppingAPI.Application.Repositories.Productt
{
    public interface IProductRepository : IRepository<Product>
    {
        public IQueryable<ProductWithCategoryNamesDTO> GetProductsWithCategory(Expression<Func<Product, bool>> filter = null);

        //public Task<List<ProductWithCategoryNamesDTO>> GetProductsWithCategoryAsync();
        public Task AddProductWithCategories(DTOs.CreatedProductDto p);
        public void ExportToDocument();

        public Task<bool> UpdateProduct(int id, UpdatedProductDto updatedProductDto);

    }
}
