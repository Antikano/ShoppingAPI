using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Application.Abstraction.Services;
using ShoppingAPI.Application.Repositories.Categoryy;
using ShoppingAPI.Domain.Entities;
using ShoppingAPI.Infrastructure.Services.Caching;

namespace ShoppingAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        //private readonly ICacheService _cacheService;

        public CategoryController(ICategoryRepository categoryRepository/*, ICacheService cacheService*/)
        {
            _categoryRepository = categoryRepository;
            //_cacheService = cacheService;
           
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var categories = _categoryRepository.GetAll();

            if (categories is null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu.");

            return Ok(categories);
        }


        //[HttpGet]
        //public async Task<IActionResult> GetAllProducts()
        //{

        //    var categories = await _cacheService.GetOrAddAsync<List<Category>>("categories", async () =>
        //    {

        //        var dbCategories = await _categoryRepository.GetAllAsync();


        //        return await dbCategories.ToListAsync();
        //    });


        //    if (categories is null)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu.");
        //    }
        //    return Ok(categories);
        //}


    }
}
