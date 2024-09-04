using Microsoft.EntityFrameworkCore;
using TodoApi.Exceptions;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepository(TodoContext context) : ITodoRepository
    {
        public async Task<List<Todo>> GetAll()
        {
            return await context.TodoItems.ToListAsync();
        }

        public async Task<Todo?> GetById(Guid id)
        {
            return await context.TodoItems.FindAsync(id);
        }

        public async Task Insert(Todo todo)
        {
            await context.TodoItems.AddAsync(todo);
            await context.SaveChangesAsync();
        }

        public async Task Update(Guid id, Todo fields)
        {
            var todo = await context.TodoItems.FindAsync(id);

            if (todo is null) {
                throw new TodoNotFoundException();
            }

            todo.Name = fields.Name;
            todo.IsComplete = fields.IsComplete;

            context.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            var todo = await context.TodoItems.FindAsync(id);

            if (todo == null)
            {
                throw new TodoNotFoundException();
            }

            context.TodoItems.Remove(todo);
            //_context.TodoItems.RemoveRange(_context.TodoItems.Where(c => c.Id == id));
            await context.SaveChangesAsync();

            //await _context.TodoItems.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
    }
}
