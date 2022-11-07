using Microsoft.EntityFrameworkCore;
using PetStore.Abstraction.DAL;
using PetStore.Common.Models;
using PetStore.DAL.Configuration;
using PetStore.Domain;

namespace PetStore.DAL.Repositories
{
    public class ToyRepository : BaseRepository<Toy, PetStoreDbContext>, IToyRepository
    {
        public ToyRepository(DbContext context) : base(context)
        {
        }

        public async Task<Toy> GetByIdAsync(int id, bool throwExceptionOnNull = true, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (throwExceptionOnNull && entity == null)
                throw new PSNotFoundException($"Toy not found by the specified id. Id: {id}");
            return entity;
        }

        public async Task<ICollection<Toy>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken = default)
        {
            var entities = await _dbSet
                .Where(x => x.IsActive == true)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);
            return entities;
        }

        public async Task<int> InsertAsync(Toy toy, CancellationToken cancellationToken = default)
        {
            if (toy == null)
            {
                throw new PSArgumentNullException("Cannot insert toy without providing the value");
            }

            _dbSet.Add(toy);
            await _context.SaveChangesAsync(cancellationToken);
            return toy.Id;
        }

        public async Task<ICollection<Toy>> SearchToysAsync(SearchToysFilter query, CancellationToken cancellationToken = default) {
            var entitiesQuery = _dbSet.AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Type)
                .Where(x => x.IsActive == true);

            if (!string.IsNullOrWhiteSpace(query.Name))
                entitiesQuery = entitiesQuery.Where(x => x.Name.ToLower().Contains(query.Name.ToLower()));

            if (query.CategoryId != null)
                entitiesQuery = entitiesQuery.Where(x => x.CategoryId == query.CategoryId);

            if (query.TypeId != null)
                entitiesQuery = entitiesQuery.Where(x => x.TypeId == query.TypeId);

            if (query.IsActive != null)
                entitiesQuery = entitiesQuery.Where(x => x.IsActive == query.IsActive);

            var entities = await entitiesQuery.ToListAsync(cancellationToken);

            return entities;
        }

        public async Task UpdateAsync(Toy toy, CancellationToken cancellationToken = default)
        {
            if (toy == null)
                throw new PSNotFoundException($"Trying to update toy which is not provided");

            _dbSet.Update(toy);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        public async Task RemoveAsync(Toy toy, CancellationToken cancellationToken = default)
        {
            if (toy == null)
                throw new PSNotFoundException($"Trying to remove toy which is not provided");

            if (toy.IsActive != true)
                return;

            toy.IsActive = false;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
