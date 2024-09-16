using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        abstract Task<User?> GetByEmail(string email);
    }
}
