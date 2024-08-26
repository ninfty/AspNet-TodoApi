using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TodoApi.Exceptions;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private ITodoRepository _todoRepository;
        
        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<List<Todo>> ListTodoItems()
        {
            return await _todoRepository.GetAll();
        }

        public async Task<Todo> FindTodoItem(Guid id)
        {
            var todo = await _todoRepository.GetById(id);

            if (todo == null) {
                throw new TodoNotFoundException();
            }

            return todo;
        }

        public async Task CreateTodoItem(Todo todo)
        {
            await _todoRepository.Insert(todo);
        }

        public async Task UpdateTodoItem(Guid id, Todo todo)
        {
            await _todoRepository.Update(id, todo);
        }

        public async Task DeleteTodoItem(Guid id)
        {
            await _todoRepository.Delete(id);
        }

    }
}
