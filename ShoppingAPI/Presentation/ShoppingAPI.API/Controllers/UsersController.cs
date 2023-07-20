using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Application.Abstraction.Token;
using ShoppingAPI.Application.DTOs;
using ShoppingAPI.Application.Repositories.Baskett;
using ShoppingAPI.Domain.DTOs;
using ShoppingAPI.Domain.Entities;
using ShoppingAPI.Domain.Entities.Identity;
using ShoppingAPI.Domain.Entities.Identity;

namespace ShoppingAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IBasketRepository _basketService;

        public UsersController(UserManager<AppUser> userManager
                              ,SignInManager<AppUser> signInManager
                              ,ITokenHandler tokenHandler
                              ,IBasketRepository basketService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _basketService = basketService;
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
                Name= user.Firstname,
                Surname= user.Surname
            },user.Password);;

            if(result.Succeeded)
            {
                AppUser createdUser = await _userManager.FindByEmailAsync(user.Email);

                bool createdBasket = await _basketService.AddAsync(new() { 
                    User=createdUser });

                

                return Ok();
            }

            return BadRequest();


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto user)
        {

            AppUser _user = await _userManager.FindByNameAsync(user.Username);
            if (_user == null) throw new Exception("kullanıcı bulunamadı");

            var basketId = _basketService.GetAll(x=>x.UserId == _user.Id).FirstOrDefault();

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(_user, user.Password,false);

            if(result.Succeeded) {
                Token token = _tokenHandler.CreateAccessToken(basketId.Id);
                return Ok(token);

            }
            return BadRequest("login işlemi hata");
            
        }


    }
}
