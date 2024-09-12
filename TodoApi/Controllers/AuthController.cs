using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                string token = await authService.LoginUser(loginRequestDTO.Email, loginRequestDTO.Password);

                return Ok(token);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(RegisterRequestDTO registerRequestDTO)
        {
            var user = mapper.Map<User>(registerRequestDTO);

            await authService.RegisterUser(user);

            return Created();
        }
    }
}
