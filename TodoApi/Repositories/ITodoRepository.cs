using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITodoRepository
    {
        public TodoItem Insert(TodoItem item);
        public TodoItem? Update(long id, TodoItem item);
        public bool Delete(long id);
        public TodoItem? GetById(long id);
        public IEnumerable<TodoItem> GetAll();
    }
}
