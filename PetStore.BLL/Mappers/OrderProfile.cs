using AutoMapper;
using PetStore.Abstraction.DAL;
using PetStore.BLL.Orders.Commands;
using PetStore.BLL.Orders.Queries;
using PetStore.DataContracts.Orders;
using PetStore.Domain;

namespace PetStore.BLL.Mappers
{
    /// <summary>
    /// Order mapper
    /// </summary>
    public class OrderProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderProfile() {
            CreateMap<Order, GetOrderResult>()
                .ForMember(dest => dest.Status, opts => opts.MapFrom((src, dest) => src.Status?.Name));
            CreateMap<CreateOrderCommand, Order>()
                .ForMember(dest => dest.Toys, opts => opts.Ignore());
            CreateMap<SearchOrdersQuery, SearchOrdersFilter>();
        }
    }
}
