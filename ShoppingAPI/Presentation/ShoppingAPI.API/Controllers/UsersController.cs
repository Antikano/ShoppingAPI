using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Domain.DTOs;
using ShoppingAPI.Domain.Entities.Identity;

namespace ShoppingAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }



       
       
       
        //[HttpGet] siliyor nice
        //public async Task denemeAsync() {
        //    var user = await _userManager.FindByIdAsync("1");
        //   await _userManager.DeleteAsync(user);

        //}

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto user)
        {
            
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                UserName = user.Username,
                Email = user.Email,
                Name= user.Name,
                Surname= user.Surname,

            },user.Password);

            if(result.Succeeded)
            {
                return Ok("succeeded");
            }

            return BadRequest();


        }


    }
}
