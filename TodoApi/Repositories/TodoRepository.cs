using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public bool Delete(Guid id)
        {
            var todoItem = _context.TodoItems.Find(id);

            if (todoItem == null) {
                return false;
            }

            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();

            return true;
        }

        public IEnumerable<Todo> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        public Todo? GetById(Guid id)
        {
            return _context.TodoItems.Find(id);
        }

        public Todo Insert(Todo todoItem)
        {
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();

            return todoItem;
        }

        public Todo? Update(Guid id, Todo fields)
        {
            var todoItem = _context.TodoItems.Find(id);

            if (todoItem == null) {
                return null;
            }

            todoItem.Name = fields.Name;
            todoItem.IsComplete = fields.IsComplete;

            _context.SaveChanges();

            return todoItem;
        }
    }
}
