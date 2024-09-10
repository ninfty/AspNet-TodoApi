using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TodoApi.Exceptions;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class TodoService(ITodoRepository todoRepository) : ITodoService
    {
        public async Task<List<Todo>> ListTodoItems()
        {
            return await todoRepository.GetAllAsync();
        }

        public async Task<Todo> FindTodoItem(Guid id)
        {
            var todo = await todoRepository.GetByIdAsync(id);

            if (todo == null) {
                throw new TodoNotFoundException();
            }

            return todo;
        }

        public async Task CreateTodoItem(Todo todo)
        {
            await todoRepository.InsertAsync(todo);
        }

        public async Task UpdateTodoItem(Guid id, Todo fields)
        {
            var todo = await todoRepository.GetByIdAsync(id);

            if (todo == null)
            {
                throw new TodoNotFoundException();
            }

            fields.Id = id;

            await todoRepository.UpdateAsync(fields);
        }

        public async Task DeleteTodoItem(Guid id)
        {
            await todoRepository.DeleteAsync(id);
        }

    }
}
