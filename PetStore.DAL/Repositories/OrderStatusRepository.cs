using Microsoft.EntityFrameworkCore;
using PetStore.Abstraction.DAL;
using PetStore.Common.Models;
using PetStore.DAL.Configuration;
using PetStore.Domain;

namespace PetStore.DAL.Repositories
{
    public class OrderStatusRepository : BaseRepository<OrderStatus, PetStoreDbContext>, IOrderStatusRepository
    {
        public OrderStatusRepository(DbContext context) : base(context)
        {
        }

        public async Task<OrderStatus> GetByIdAsync(int id, bool throwExceptionOnNull = true, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (throwExceptionOnNull && entity == null)
                throw new PSNotFoundException($"Order status not found by the specified id. Id: {id}");
            return entity;
        }
    }
}
