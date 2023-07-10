using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Application.Repositories.Productt;
using ShoppingAPI.Domain.DTOs;
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

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var products = _productRepository.GetAll();
        //    return Ok(products);
        //}

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
            var product = new Product
            {
                Name = p.Name,
                Description = p.Description,
                Stock = p.Stock,
                Price = p.Price,
                ImageData=p.ImageData
              
            };

            await _productRepository.AddAsync(product);
            return Ok();


        }
    }
}
