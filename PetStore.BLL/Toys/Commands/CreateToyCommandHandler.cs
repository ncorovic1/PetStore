using AutoMapper;
using MediatR;
using PetStore.Abstraction.DAL;
using PetStore.Common.Models;
using PetStore.Domain;

namespace PetStore.BLL.Toys.Commands
{
    public class CreateToyCommandHandler : IRequestHandler<CreateToyCommand, BaseResponse<int>>
    {
        private readonly IToyRepository _toyRepository;
        private readonly IMapper _mapper;

        public CreateToyCommandHandler(
            IToyRepository toyRepository,
            IMapper mapper
        )
        {
            _toyRepository = toyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new toy
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<BaseResponse<int>> Handle(CreateToyCommand command, CancellationToken cancellationToken)
        {
            var toy = _mapper.Map<CreateToyCommand, Toy>(command);
            var id = await _toyRepository.InsertAsync(toy, cancellationToken);
            return new BaseResponse<int> { Data = id };
        }
    }
}
    