using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        public Task<List<Todo>> ListTodoItems();
        public Task<Todo> FindTodoItem(Guid id);
        public Task CreateTodoItem(Todo todo);
        public Task UpdateTodoItem(Guid id, Todo todo);
        public Task DeleteTodoItem(Guid id);
    }
}
