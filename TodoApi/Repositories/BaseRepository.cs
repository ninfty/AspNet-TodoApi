using Microsoft.EntityFrameworkCore;
using TodoApi.Database;

namespace TodoApi.Repositories
{
    public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
    {
        public DbSet<T> entity => context.Set<T>();

        public async Task DeleteAsync(object id)
        {
            var entity = await context.FindAsync<T>(id);

            if (entity != null)
            {
                this.entity.Remove(entity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await context.FindAsync<T>(id);
        }

        public async Task InsertAsync(T entity)
        {
            this.entity.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
