using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITodoRepository
    {
        public Todo Insert(Todo item);
        public Todo? Update(Guid id, Todo item);
        public bool Delete(Guid id);
        public Todo? GetById(Guid id);
        public IEnumerable<Todo> GetAll();
    }
}
