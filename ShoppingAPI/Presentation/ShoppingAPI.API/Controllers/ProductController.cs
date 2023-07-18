using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Application.DTOs;
using ShoppingAPI.Application.Repositories.Productt;

using ShoppingAPI.Domain.Entities;

namespace ShoppingAPI.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    
    public class ProductController : ControllerBase
    {
        readonly private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var product = await _productRepository.GetAsync(p => p.Id == id);

            if (product is null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu.");        

            return Ok(product);
        }

        [HttpGet("allProduct")]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetAll();

            if (products is null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu.");

            return Ok(products);
        }

        [Authorize(AuthenticationSchemes = "authScheme")]
        [HttpGet("withCategoryName")]
        public IActionResult GetAllProductsWithCategoryName()
        {
            var products = _productRepository.GetProductsWithCategory();

            if (products is null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu.");

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatedProductDto p)
        {
             _productRepository.AddProductWithCategories(p); // bu async çalışmadı bi bak neden! kötü kod !

            return Ok();
        }

    }
}
