using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        public IEnumerable<Todo> ListTodoItems();
        public Todo FindTodoItem(Guid id);
        public void UpdateTodoItem(Guid id, Todo todo);
        public Todo CreateTodoItem(Todo todo);
        public bool DeleteTodoItem(Guid id);
    }
}
