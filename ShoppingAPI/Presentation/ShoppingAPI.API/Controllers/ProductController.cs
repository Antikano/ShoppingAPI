using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Application.DTOs;
using ShoppingAPI.Application.Repositories.Categoryy;
using ShoppingAPI.Application.Repositories.Productt;

namespace ShoppingAPI.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        readonly private IProductRepository _productRepository;
        readonly private ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
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
            try
            {
                await _productRepository.AddProductWithCategories(p);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdatedProductDto updatedProductDto)
        {
           var den = await _productRepository.UpdateProduct(id, updatedProductDto);

            if (den is false)
            {
                throw new Exception();
            }

            return Ok();
        }
    }
}
