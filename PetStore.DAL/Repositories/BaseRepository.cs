using Microsoft.EntityFrameworkCore;

namespace PetStore.DAL.Repositories
{
    public abstract class BaseRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        protected TContext _context;
        protected DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = (TContext)context;
            _dbSet = context.Set<TEntity>();
        }
    }
}
