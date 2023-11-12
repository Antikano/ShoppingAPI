using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Application.DTOs;
using ShoppingAPI.Application.Repositories.Baskett;

namespace ShoppingAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBasket(int id)
        {

            var basket = await _basketRepository.BasketWithProducts(id);

            if (basket == null)
            {
                return NotFound();
            }

            return Ok(basket);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBasket(int id, [FromBody] UpdatedBasketDto updatedBasket)
        {

            _basketRepository.updateBasket(id, updatedBasket);

            return Ok();
        }

        [HttpPost]
        public IActionResult CreateOrder(OrderDto orderDto)
        {
            if (orderDto is null)
                return BadRequest();

            _basketRepository.ExportToDocument(orderDto);

            return Ok();
        }
    }
}
