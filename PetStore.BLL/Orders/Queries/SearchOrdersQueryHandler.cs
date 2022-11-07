using AutoMapper;
using MediatR;
using PetStore.Abstraction.DAL;
using PetStore.DataContracts.Orders;

namespace PetStore.BLL.Orders.Queries
{
    public class SearchOrdersQueryHandler : IRequestHandler<SearchOrdersQuery, ICollection<GetOrderResult>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public SearchOrdersQueryHandler(
            IMapper mapper,
            IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<ICollection<GetOrderResult>> Handle(SearchOrdersQuery query, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<SearchOrdersQuery, SearchOrdersFilter>(query);
            var orders = await _orderRepository.SearchOrdersAsync(filter, cancellationToken);
            var result = _mapper.Map<ICollection<GetOrderResult>>(orders);
            return result;
        }
    }
}
