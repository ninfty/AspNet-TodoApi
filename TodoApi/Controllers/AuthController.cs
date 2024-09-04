using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {

        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(LoginRequestDTO loginRequestDTO)
        {
            //var todo = _mapper.Map<Todo>(todoRequestDTO);

            //await _todoItemsService.CreateTodoItem(todo);

            return Ok();
        }
    }
}
