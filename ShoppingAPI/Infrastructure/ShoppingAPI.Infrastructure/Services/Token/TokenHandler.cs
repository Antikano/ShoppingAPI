using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingAPI.Application.Abstraction.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ShoppingAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Application.DTOs.Token CreateAccessToken(int basketID)
        {
            Application.DTOs.Token token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentialss = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken jwtSecurityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentialss
                );
            jwtSecurityToken.Payload["basketID"] = basketID;

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
