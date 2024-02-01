using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UPG3ButWithTests.Services.Interfaces;

namespace UPG3ButWithTests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILoginService loginService) : ControllerBase
    {
        private readonly ILoginService _loginService = loginService;
        [HttpPost("Login")]
        public IActionResult Login(string loginKey)
        {
            string token = _loginService.Login(loginKey);
            if (token != "Invalid Credentials")
            {
                return Ok(token);
            }
            return BadRequest(token);
        }
    }
}
