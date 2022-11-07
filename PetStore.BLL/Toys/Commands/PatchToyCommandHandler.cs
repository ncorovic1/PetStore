using AutoMapper;
using MediatR;
using PetStore.Abstraction.DAL;

namespace PetStore.BLL.Toys.Commands
{
    public class PatchToyCommandHandler : IRequestHandler<PatchToyCommand, Unit>
    {
        private readonly IToyRepository _toyRepository;
        private readonly IMapper _mapper;

        public PatchToyCommandHandler(
            IToyRepository toyRepository,
            IMapper mapper
        )
        {
            _toyRepository = toyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Update toy fully or partially
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<Unit> Handle(PatchToyCommand command, CancellationToken cancellationToken)
        {
            var toy = await _toyRepository.GetByIdAsync(command.Id, true, cancellationToken);
            _mapper.Map(command, toy);
            await _toyRepository.UpdateAsync(toy, cancellationToken);
            return Unit.Value;
        }
    }
}
    