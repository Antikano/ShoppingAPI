using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Application.Abstraction.Services;
using ShoppingAPI.Application.Repositories.Categoryy;
using ShoppingAPI.Domain.Entities;
using ShoppingAPI.Infrastructure.Caching;

namespace ShoppingAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var categories = _categoryRepository.getCategoriesFromRedis();

            return Ok(categories);
        }

    }
}
