using Microsoft.EntityFrameworkCore;
using PetStore.Abstraction.DAL;
using PetStore.Common.Models;
using PetStore.DAL.Configuration;
using PetStore.Domain;

namespace PetStore.DAL.Repositories
{
    public class ToyTypeRepository : BaseRepository<ToyType, PetStoreDbContext>, IToyTypeRepository
    {
        public ToyTypeRepository(DbContext context) : base(context)
        {
        }

        public async Task<ToyType> GetByIdAsync(int id, bool throwExceptionOnNull = true, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (throwExceptionOnNull && entity == null)
                throw new PSNotFoundException($"Toy type not found by the specified id. Id: {id}");
            return entity;
        }
    }
}
