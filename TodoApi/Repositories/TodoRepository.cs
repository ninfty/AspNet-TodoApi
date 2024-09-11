using Microsoft.EntityFrameworkCore;
using TodoApi.Database;
using TodoApi.Exceptions;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
