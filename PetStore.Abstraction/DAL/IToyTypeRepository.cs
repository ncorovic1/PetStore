using PetStore.Domain;

namespace PetStore.Abstraction.DAL
{
    public interface IToyTypeRepository
    {
        /// <summary>
        /// Retrieves single toy type based on id
        /// </summary>
        /// <param name="id">Toy type id</param>
        /// <param name="throwExceptionOnNull">Flag indicating if not found exception will be thrown on null</param>
        /// <returns></returns>
        Task<ToyType> GetByIdAsync(int id, bool throwExceptionOnNull = true, CancellationToken cancellationToken = default);
    }
}
