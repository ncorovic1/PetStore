using PetStore.Domain;

namespace PetStore.Abstraction.DAL
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Retrieves orders based on a search filter
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>Collection of orders</returns>
        Task<ICollection<Order>> SearchOrdersAsync(SearchOrdersFilter query, CancellationToken cancellationToken = default);
        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="order">Order data</param>
        /// <returns>Id</returns>
        Task<int> InsertAsync(Order order, CancellationToken cancellationToken = default);
    }
}
