using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.DTOs;
using TodoApi.Exceptions;
using TodoApi.Models;
using TodoApi.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(ITodoService todoService, IMapper mapper) : ControllerBase
    {
        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoResponseDTO>>> GetTodoItems()
        {
            //return (await _todoItemsService.ListTodoItems()).Select(x => _mapper.Map<TodoResponseDTO>(x)).ToList();

            var todos = await todoService.ListTodoItems();

            return Ok(mapper.Map<List<TodoResponseDTO>>(todos));
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoResponseDTO>> GetTodoItem(Guid id)
        {
            try
            {
                return mapper.Map<TodoResponseDTO>(
                   await todoService.FindTodoItem(id)
                );
            } catch (TodoNotFoundException) {
                return NotFound();
            }
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoRequestDTO todoRequestDTO)
        {
            try {
                await todoService.UpdateTodoItem(id, mapper.Map<Todo>(todoRequestDTO));

                return NoContent();
            }
            catch (TodoNotFoundException) {
                return NotFound();
            }
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoResponseDTO>> PostTodoItem(TodoRequestDTO todoRequestDTO)
        {
            var todo = mapper.Map<Todo>(todoRequestDTO);

            await todoService.CreateTodoItem(todo);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todo.Id },
                mapper.Map<TodoResponseDTO>(todo)
            );
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            try {
                await todoService.DeleteTodoItem(id);
            } catch (Exception) {
                return NotFound();
            }

            return NoContent();
        }
    }
}
