using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<string>> Post(LoginRequestDTO loginRequestDTO)
        {
            await authService.LoginUser(loginRequestDTO.Email, loginRequestDTO.Password);

            return Ok();
        }
    }
}
