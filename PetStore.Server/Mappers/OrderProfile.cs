using AutoMapper;
using PetStore.BLL.Orders.Commands;
using PetStore.BLL.Orders.Queries;
using PetStore.DataContracts.Orders;

namespace PetStore.Server.Mappers
{
    /// <summary>
    /// Order mapper
    /// </summary>
    public class OrderProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderProfile()
        {
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<SearchOrdersRequest, SearchOrdersQuery>();
        }
    }
}
