using MediatR;
using PetStore.Common.Models;
using PetStore.DataContracts.Orders;

namespace PetStore.BLL.Orders.Commands
{
    public class CreateOrderCommand : CreateOrderRequest, IRequest<BaseResponse<int>>
    {
    }
}
