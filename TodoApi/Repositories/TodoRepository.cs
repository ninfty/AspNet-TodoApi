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

        public bool Delete(long id)
        {
            var todoItem = _context.TodoItems.Find(id);

            if (todoItem == null) {
                return false;
            }

            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();

            return true;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        public TodoItem? GetById(long id)
        {
            return _context.TodoItems.Find(id);
        }

        public TodoItem Insert(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();

            return todoItem;
        }

        public TodoItem? Update(long id, TodoItem fields)
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
