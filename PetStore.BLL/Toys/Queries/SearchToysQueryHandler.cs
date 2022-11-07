using AutoMapper;
using MediatR;
using PetStore.Abstraction.DAL;
using PetStore.DataContracts.Toys;

namespace PetStore.BLL.Toys.Queries
{
    public class SearchToysQueryHandler : IRequestHandler<SearchToysQuery, ICollection<GetToyResult>>
    {
        private readonly IMapper _mapper;
        private readonly IToyRepository _toyRepository;

        public SearchToysQueryHandler(
            IMapper mapper,
            IToyRepository toyRepository)
        {
            _mapper = mapper;
            _toyRepository = toyRepository;
        }

        public async Task<ICollection<GetToyResult>> Handle(SearchToysQuery query, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<SearchToysQuery, SearchToysFilter>(query);
            var toys = await _toyRepository.SearchToysAsync(filter, cancellationToken);
            var result = _mapper.Map<ICollection<GetToyResult>>(toys);
            return result;
        }
    }
}
