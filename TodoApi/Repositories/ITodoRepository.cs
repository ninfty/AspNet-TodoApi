using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITodoRepository
    {
        public Task<List<Todo>> GetAll();
        public Task<Todo?> GetById(Guid id);
        public Task Insert(Todo item);
        public Task Update(Guid id, Todo item);
        public Task Delete(Guid id);
    }
}
