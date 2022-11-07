using PetStore.Domain;

namespace PetStore.Abstraction.DAL
{
    public interface IToyCategoryRepository
    {
        /// <summary>
        /// Retrieves single toy category based on id
        /// </summary>
        /// <param name="id">Toy category id</param>
        /// <param name="throwExceptionOnNull">Flag indicating if not found exception will be thrown on null</param>
        /// <returns></returns>
        Task<ToyCategory> GetByIdAsync(int id, bool throwExceptionOnNull = true, CancellationToken cancellationToken = default);
    }
}
