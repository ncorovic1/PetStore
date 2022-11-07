using MediatR;
using PetStore.DataContracts.Toys;

namespace PetStore.BLL.Toys.Commands
{
    public class PatchToyCommand : PatchToyRequest, IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
