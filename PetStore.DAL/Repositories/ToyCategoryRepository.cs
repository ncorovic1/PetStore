using Microsoft.EntityFrameworkCore;
using PetStore.Abstraction.DAL;
using PetStore.Common.Models;
using PetStore.DAL.Configuration;
using PetStore.Domain;

namespace PetStore.DAL.Repositories
{
    public class ToyCategoryRepository : BaseRepository<ToyCategory, PetStoreDbContext>, IToyCategoryRepository
    {
        public ToyCategoryRepository(DbContext context) : base(context)
        {
        }

        public async Task<ToyCategory> GetByIdAsync(int id, bool throwExceptionOnNull = true, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (throwExceptionOnNull && entity == null)
                throw new PSNotFoundException($"Toy category not found by the specified id. Id: {id}");
            return entity;
        }
    }
}
