using AutoMapper;
using PetStore.BLL.Toys.Commands;
using PetStore.BLL.Toys.Queries;
using PetStore.DataContracts.Toys;

namespace PetStore.Server.Mappers
{
    /// <summary>
    /// Toy mapper
    /// </summary>
    public class ToyProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ToyProfile()
        {
            CreateMap<CreateToyRequest, CreateToyCommand>();
            CreateMap<PatchToyRequest, PatchToyCommand>();
            CreateMap<SearchToysRequest, SearchToysQuery>();
        }
    }
}
