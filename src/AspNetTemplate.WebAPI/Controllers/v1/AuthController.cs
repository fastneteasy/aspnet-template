using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspNetTemplate.WebAPI.Controllers.v1
{
    [Route("api/v1/auth")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController(
        ILoggerFactory loggerFactory,
        IConfiguration configuration)
        : BaseController(loggerFactory)
    {
        [HttpPost]
        public async Task<string> Login()
        {

            List<Claim> claims =
                [new Claim(ClaimTypes.NameIdentifier, "user id"), new Claim(ClaimTypes.Name, "user name")];

            var key = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configuration.GetSection("JWTSecurityKey").Get<string>()!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(string.Empty,
                string.Empty,
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}