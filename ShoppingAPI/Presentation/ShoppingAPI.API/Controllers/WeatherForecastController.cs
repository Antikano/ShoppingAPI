using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Application.Repositories.Productt;
using ShoppingAPI.Domain.Entities;

namespace ShoppingAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        readonly private IProductRepository _productRepository;

        public WeatherForecastController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetAll();

            if (products is null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluþtu.");

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var product = await _productRepository.GetAsync(p => p.Id == id);

            if (product is null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluþtu.");

            return Ok(product);
        }

        //[HttpGet]
        //public async Task deneme()
        //{
        //    await _productRepository.AddAsync(new() {Name= "Canon EOS 80D",
        //        Description= "Advanced DSLR camera for professional photographers",
        //    Category= "Electronics", Stock=48, Price=1400
        //    });
        //}

    }
}