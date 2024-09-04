using Microsoft.EntityFrameworkCore;
using TodoApi.Database;
using TodoApi.Exceptions;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepository(AppDbContext context) : ITodoRepository
    {
        public async Task<List<Todo>> GetAll()
        {
            return await context.Todos.ToListAsync();
        }

        public async Task<Todo?> GetById(Guid id)
        {
            return await context.Todos.FindAsync(id);
        }

        public async Task Insert(Todo todo)
        {
            await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();
        }

        public async Task Update(Guid id, Todo fields)
        {
            var todo = await context.Todos.FindAsync(id);

            if (todo is null) {
                throw new TodoNotFoundException();
            }

            todo.Name = fields.Name;
            todo.IsComplete = fields.IsComplete;

            context.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            var todo = await context.Todos.FindAsync(id);

            if (todo == null)
            {
                throw new TodoNotFoundException();
            }

            context.Todos.Remove(todo);
            //_context.TodoItems.RemoveRange(_context.TodoItems.Where(c => c.Id == id));
            await context.SaveChangesAsync();

            //await _context.TodoItems.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
    }
}
