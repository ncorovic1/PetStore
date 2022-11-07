using Microsoft.EntityFrameworkCore;
using PetStore.Abstraction.DAL;
using PetStore.Common.Models;
using PetStore.DAL.Configuration;
using PetStore.Domain;

namespace PetStore.DAL.Repositories
{
    public class OrderRepository : BaseRepository<Order, PetStoreDbContext>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }

        public async Task<int> InsertAsync(Order order, CancellationToken cancellationToken)
        {
            if (order == null)
            {
                throw new PSArgumentNullException("Cannot insert order without providing the value");
            }

            _dbSet.Add(order);
            await _context.SaveChangesAsync(cancellationToken);
            return order.Id;
        }

        public async Task<ICollection<Order>> SearchOrdersAsync(SearchOrdersFilter query, CancellationToken cancellationToken) {
            var entitiesQuery = _dbSet.AsNoTracking();
            entitiesQuery = entitiesQuery.Include(x => x.Status)
                                         .Include(x => x.Toys);

            if (!string.IsNullOrWhiteSpace(query.FirstName))
                entitiesQuery = entitiesQuery.Where(x => x.FirstName.ToLower().Contains(query.FirstName.ToLower()));

            if (!string.IsNullOrWhiteSpace(query.LastName))
                entitiesQuery = entitiesQuery.Where(x => x.LastName.ToLower().Contains(query.LastName.ToLower()));

            if (query.StatusId != null)
                entitiesQuery = entitiesQuery.Where(x => x.StatusId == query.StatusId);

            if (!string.IsNullOrWhiteSpace(query.City))
                entitiesQuery = entitiesQuery.Where(x => x.City.ToLower().Contains(query.City.ToLower()));

            var entities = await entitiesQuery.ToListAsync(cancellationToken);

            return entities;
        }
    }
}
