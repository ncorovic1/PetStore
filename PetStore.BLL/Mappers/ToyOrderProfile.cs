using AutoMapper;
using PetStore.DataContracts.ToyOrders;
using PetStore.Domain;

namespace PetStore.BLL.Mappers
{
    /// <summary>
    /// Basket mapper
    /// </summary>
    public class ToyOrderProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ToyOrderProfile() {
            CreateMap<ToyOrder, GetToyOrderResult>();
        }
    }
}
