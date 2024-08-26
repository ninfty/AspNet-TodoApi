using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoItemsService;
        private IMapper _mapper;

        public TodoController(ITodoService todoItemsService, IMapper mapper)
        {
            _todoItemsService = todoItemsService;
            _mapper = mapper;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoResponseDTO>>> GetTodoItems()
        {
            //return (await _todoItemsService.ListTodoItems()).Select(x => _mapper.Map<TodoResponseDTO>(x)).ToList();

            var todos = await _todoItemsService.ListTodoItems();

            return Ok(_mapper.Map<List<TodoResponseDTO>>(todos));
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoResponseDTO>> GetTodoItem(Guid id)
        {
            try
            {
                return _mapper.Map<TodoResponseDTO>(
                   await _todoItemsService.FindTodoItem(id)
                );
            } catch (Exception) {
                return NotFound();
            }
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoRequestDTO todoRequestDTO)
        {
            try {
                await _todoItemsService.UpdateTodoItem(id, _mapper.Map<Todo>(todoRequestDTO));

                return NoContent();
            }
            catch (Exception) {
                return NotFound();
            }
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoResponseDTO>> PostTodoItem(TodoRequestDTO todoRequestDTO)
        {
            var todo = _mapper.Map<Todo>(todoRequestDTO);

            await _todoItemsService.CreateTodoItem(todo);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todo.Id },
                _mapper.Map<TodoResponseDTO>(todo)
            );
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            try {
                await _todoItemsService.DeleteTodoItem(id);
            } catch (Exception) {
                return NotFound();
            }

            return NoContent();
        }
    }
}
