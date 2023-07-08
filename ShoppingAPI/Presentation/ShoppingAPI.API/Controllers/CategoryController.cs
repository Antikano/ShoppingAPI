using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Application.Repositories.Categoryy;

namespace ShoppingAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
                _categoryRepository= categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var categories = _categoryRepository.GetAll();

            if (categories is null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu.");

            return Ok(categories);
        }


    }
}
