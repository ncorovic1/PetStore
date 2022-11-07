using MediatR;

namespace PetStore.BLL.Toys.Commands
{
    public class DeleteToyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
