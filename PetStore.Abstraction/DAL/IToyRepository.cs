using PetStore.Domain;

namespace PetStore.Abstraction.DAL
{
    public interface IToyRepository
    {
        /// <summary>
        /// Retrieves toys based on a search filter
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>Collection of toys</returns>
        Task<ICollection<Toy>> SearchToysAsync(SearchToysFilter query, CancellationToken cancellationToken = default);
        /// <summary>
        /// Creates a new toy
        /// </summary>
        /// <param name="toy">Toy data</param>
        /// <returns>Id</returns>
        Task<int> InsertAsync(Toy toy, CancellationToken cancellationToken = default);
        /// <summary>
        /// Retrieves single toy based on id
        /// </summary>
        /// <param name="id">Toy id</param>
        /// <param name="throwExceptionOnNull">Flag indicating if not found exception will be thrown on null</param>
        /// <returns></returns>
        Task<Toy> GetByIdAsync(int id, bool throwExceptionOnNull = true, CancellationToken cancellationToken = default);
        /// <summary>
        /// Retrieves collection of toys based on collection of ids
        /// </summary>
        /// <param name="id">Toy ids</param>
        /// <returns></returns>
        Task<ICollection<Toy>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates existing toy
        /// </summary>
        /// <param name="toy">Toy data</param>
        /// <returns>Id</returns>
        Task UpdateAsync(Toy toy, CancellationToken cancellationToken = default);
        /// <summary>
        /// Removes existing toy
        /// </summary>
        /// <param name="toy">Toy data</param>
        /// <returns></returns>
        Task RemoveAsync(Toy toy, CancellationToken cancellationToken = default);
    }
}
