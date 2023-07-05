using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Application.Repositories.Prodcut;

namespace ShoppingAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        

        readonly private IProductRepository _productRepository;

        public WeatherForecastController( IProductRepository productRepository)
        {
          

            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetAll();
            return Ok(products);
        }

    }
}