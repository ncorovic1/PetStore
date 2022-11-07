using MediatR;
using PetStore.Abstraction.DAL;

namespace PetStore.BLL.Toys.Commands
{
    public class DeleteToyCommandHandler : IRequestHandler<DeleteToyCommand, Unit>
    {
        private readonly IToyRepository _toyRepository;

        public DeleteToyCommandHandler(
            IToyRepository toyRepository
        )
        {
            _toyRepository = toyRepository;
        }

        /// <summary>
        /// Remove toy
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<Unit> Handle(DeleteToyCommand command, CancellationToken cancellationToken)
        {
            var toy = await _toyRepository.GetByIdAsync(command.Id, true, cancellationToken);
            await _toyRepository.RemoveAsync(toy, cancellationToken);
            return Unit.Value;
        }
    }
}
    