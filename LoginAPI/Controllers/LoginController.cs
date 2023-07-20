using LoginAPI.Models;
using LoginAPI.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult GetToken(UserViewModel model)
        {
            var username = _configuration["Token:Username"];
            var password = _configuration["Token:Password"];
            if (model.Username == username && model.Password == password)
            {
                Token token = TokenHandler.CreateToken(_configuration);
                
                return Ok(new
                {
                    userid=1,
                    Username=username,
                    Password=password,
                    Token = token
                });
            }
            else
            {
                return BadRequest(new { Message = "Hatalı kullanıcı adı veya şifre girdiniz. Tekrar Deneyiniz." });
            }
        }
    }
}
