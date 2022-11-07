using PetStore.Domain;

namespace PetStore.Abstraction.DAL
{
    public interface IOrderStatusRepository
    {
        /// <summary>
        /// Retrieves single order status based on id
        /// </summary>
        /// <param name="id">Order status id</param>
        /// <param name="throwExceptionOnNull">Flag indicating if not found exception will be thrown on null</param>
        /// <returns></returns>
        Task<OrderStatus> GetByIdAsync(int id, bool throwExceptionOnNull = true, CancellationToken cancellationToken = default);
    }
}
