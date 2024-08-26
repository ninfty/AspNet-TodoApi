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

        public IEnumerable<Todo> ListTodoItems()
        {
            return _todoRepository.GetAll();
        }

        public Todo FindTodoItem(Guid id)
        {
            var todo = _todoRepository.GetById(id);

            if (todo == null) {
                throw new TodoNotFoundException("Not found");
            }

            return todo;
        }

        public void UpdateTodoItem(Guid id, Todo todo)
        {
            try
            {
                _todoRepository.Update(id, todo);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public Todo CreateTodoItem(Todo todo)
        {
            _todoRepository.Insert(todo);

            return todo;
        }

        public bool DeleteTodoItem(Guid id)
        {
            var deleted =  _todoRepository.Delete(id);

            if (!deleted) {
                throw new TodoNotFoundException("Not found");
            }

            return true;
        }

    }
}
