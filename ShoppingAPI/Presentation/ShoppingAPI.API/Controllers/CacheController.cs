using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Application.Abstraction.Services;
using ShoppingAPI.Application.DTOs;

namespace ShoppingAPI.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet("cache/{key}")]
        public async Task<IActionResult> Get(string key)
        {
            return Ok(await _cacheService.GetValueAsync(key));
        }

        [HttpPost("cache")]
        public async Task<IActionResult> Post([FromBody] CacheRequestModel model)
        {
            await _cacheService.SetValueAsync(model.Key, model.Value);
            return Ok();
        }
        [HttpDelete("cache/{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            await _cacheService.Clear(key);
            return Ok();
        }
    }
}
