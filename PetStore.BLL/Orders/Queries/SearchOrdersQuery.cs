using MediatR;
using PetStore.DataContracts.Orders;

namespace PetStore.BLL.Orders.Queries
{
    public class SearchOrdersQuery : SearchOrdersRequest, IRequest<ICollection<GetOrderResult>>
    {
    }
}
