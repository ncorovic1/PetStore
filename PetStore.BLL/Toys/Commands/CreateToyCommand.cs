using MediatR;
using PetStore.Common.Models;
using PetStore.DataContracts.Toys;

namespace PetStore.BLL.Toys.Commands
{
    public class CreateToyCommand : CreateToyRequest, IRequest<BaseResponse<int>>
    {
    }
}
