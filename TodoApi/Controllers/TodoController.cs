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
        public async Task<ActionResult<IEnumerable<TodoDTO>>> GetTodoItems()
        {
            return _todoItemsService.ListTodoItems()
                .Select(x => _mapper.Map<TodoDTO>(x))
                .ToList();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDTO>> GetTodoItem(Guid id)
        {
            try
            {
                var todo = _todoItemsService.FindTodoItem(id);

                return _mapper.Map<TodoDTO>(todo);
            } catch (Exception) {
                return NotFound();
            }
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoDTO todoDTO)
        {
            if (id != todoDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                _todoItemsService.UpdateTodoItem(id, _mapper.Map<Todo>(todoDTO));
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoDTO>> PostTodoItem(TodoDTO todoDTO)
        {
            var todo = _todoItemsService.CreateTodoItem(_mapper.Map<Todo>(todoDTO));

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todo.Id },
                _mapper.Map<TodoDTO>(todo)
            );
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            try {
                _todoItemsService.DeleteTodoItem(id);
            } catch (Exception) {
                return NotFound();
            }

            return NoContent();
        }
    }
}
