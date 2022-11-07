using AutoMapper;
using FluentValidation.Results;
using MediatR;
using PetStore.Abstraction.DAL;
using PetStore.Common.Models;
using PetStore.DataContracts.Orders;
using PetStore.Domain;

namespace PetStore.BLL.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, BaseResponse<int>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IToyRepository _toyRepository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            IOrderStatusRepository orderStatusRepository,
            IToyRepository toyRepository,
            IMapper mapper
        )
        {
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _toyRepository = toyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<BaseResponse<int>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            ICollection<Toy> toys = await Validate(command, cancellationToken);

            var order = _mapper.Map<CreateOrderCommand, Order>(command);
            foreach (var toy in command.Toys)
            {
                var toyEntity = toys.FirstOrDefault(x => x.Id == toy.ProductId);
                toyEntity.Quantity -= toy.Quantity;
                var toyOrder = new ToyOrder
                {
                    Toy = toyEntity,
                    Quantity = toy.Quantity
                };
                order.Toys.Add(toyOrder);
            };

            var id = await _orderRepository.InsertAsync(order, cancellationToken);
            return new BaseResponse<int> { Data = id };
        }

        /// <summary>
        /// Performs async validations 
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>Collection of toys under order</returns>
        /// <exception cref="PSValidationException"></exception>
        private async Task<ICollection<Toy>> Validate(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderStatus = await _orderStatusRepository.GetByIdAsync(command.StatusId, false, cancellationToken);
            if (orderStatus == null)
                throw new PSValidationException(new List<ValidationFailure> {
                        new() {
                            PropertyName = nameof(CreateOrderRequest.StatusId),
                            ErrorMessage = "Could not find status by the specified id"
                        }
                    }
                );

            var toyIds = command.Toys.ConvertAll(x => x.ProductId);
            var toys = await _toyRepository.GetByIdsAsync(toyIds, cancellationToken);
            if (toyIds.Count > toys.Count)
                throw new PSValidationException(new List<ValidationFailure> {
                        new() {
                            PropertyName = nameof(CreateOrderRequest.StatusId),
                            ErrorMessage = $"Could not find some of the toys by the specified ids. Ids: {string.Join(", ", toyIds)}"
                        }
                    }
                );

            foreach (var toy in toys)
            {
                var requestedQuantity = command.Toys.FirstOrDefault(x => x.ProductId == toy.Id).Quantity;
                if (toy.Quantity < requestedQuantity)
                    throw new PSValidationException(new List<ValidationFailure> {
                            new() {
                                PropertyName = nameof(CreateOrderRequest.Toys),
                                ErrorMessage = $"There is not enough toys in the system. Id: {toy.Id}. Requested: {requestedQuantity}. Available: {toy.Quantity}"
                            }
                        }
                    );
            }

            var orderAmount = command.Amount;
            double calculatedAmount = 0;
            foreach (var toy in toys)
            {
                var requestedQuantity = command.Toys.FirstOrDefault(x => x.ProductId == toy.Id).Quantity;
                calculatedAmount += toy.Price * requestedQuantity;
            }

            if (orderAmount != calculatedAmount)
                throw new PSValidationException(new List<ValidationFailure> {
                        new() {
                            PropertyName = nameof(CreateOrderRequest.Amount),
                            ErrorMessage = $"Amount is not correct. Amount: {orderAmount}. Expected amount: {calculatedAmount}"
                        }
                    });
            return toys;
        }
    }
}
