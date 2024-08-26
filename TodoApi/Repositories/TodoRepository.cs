using Microsoft.EntityFrameworkCore;
using TodoApi.Exceptions;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<Todo>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<Todo?> GetById(Guid id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task Insert(Todo todo)
        {
            await _context.TodoItems.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Guid id, Todo fields)
        {
            var todo = await _context.TodoItems.FindAsync(id);

            if (todo is null) {
                throw new TodoNotFoundException();
            }

            todo.Name = fields.Name;
            todo.IsComplete = fields.IsComplete;

            _context.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            var todo = await _context.TodoItems.FindAsync(id);

            if (todo == null)
            {
                throw new TodoNotFoundException();
            }

            _context.TodoItems.Remove(todo);
            //_context.TodoItems.RemoveRange(_context.TodoItems.Where(c => c.Id == id));
            await _context.SaveChangesAsync();

            //await _context.TodoItems.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
    }
}
