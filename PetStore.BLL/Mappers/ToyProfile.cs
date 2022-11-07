using AutoMapper;
using PetStore.Abstraction.DAL;
using PetStore.BLL.Toys.Commands;
using PetStore.BLL.Toys.Queries;
using PetStore.DataContracts.Toys;
using PetStore.Domain;

namespace PetStore.BLL.Mappers
{
    /// <summary>
    /// Toy mapper
    /// </summary>
    public class ToyProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ToyProfile() {
            CreateMap<Toy, GetToyResult>()
                .ForMember(dest => dest.Category, opts => opts.MapFrom((src, dest) => src.Category?.Name))
                .ForMember(dest => dest.Type, opts => opts.MapFrom((src, dest) => src.Type?.Name));
            CreateMap<CreateToyCommand, Toy>();
            CreateMap<PatchToyCommand, Toy>()
                .ForMember(dest => dest.Name, opts =>
                {
                    opts.Condition((src, dest) => src.Name?.DoUpdate == true);
                    opts.MapFrom(src => src.Name.Value);
                })
                .ForMember(dest => dest.CategoryId, opts =>
                {
                    opts.Condition((src, dest) => src.CategoryId?.DoUpdate == true);
                    opts.MapFrom(src => src.CategoryId.Value);
                })
                .ForMember(dest => dest.TypeId, opts =>
                {
                    opts.Condition((src, dest) => src.TypeId?.DoUpdate == true);
                    opts.MapFrom(src => src.TypeId.Value);
                })
                .ForMember(dest => dest.Price, opts =>
                {
                    opts.Condition((src, dest) => src.Price?.DoUpdate == true);
                    opts.MapFrom(src => src.Price.Value);
                })
                .ForMember(dest => dest.Quantity, opts =>
                {
                    opts.Condition((src, dest) => src.Quantity?.DoUpdate == true);
                    opts.MapFrom(src => src.Quantity.Value);
                });
            CreateMap<SearchToysQuery, SearchToysFilter>();
        }
    }
}
