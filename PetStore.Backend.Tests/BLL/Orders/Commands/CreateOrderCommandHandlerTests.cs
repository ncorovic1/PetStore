using AutoMapper;
using Moq;
using PetStore.Abstraction.DAL;
using PetStore.BLL.Orders.Commands;
using PetStore.Common.Models;
using PetStore.Domain;
using Xunit;

namespace PetStore.Backend.Tests.BLL.Orders.Commands
{
    public class CreateOrderCommandHandlerTests
    {
        private Mock<IOrderRepository> _orderRepository;
        private Mock<IOrderStatusRepository> _orderStatusRepository;
        private Mock<IToyRepository> _toyRepository;
        private Mock<IMapper> _mapper;

        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandHandlerTests()
        {
            SetupMocks();

            _handler = new CreateOrderCommandHandler(
                _orderRepository.Object,
                _orderStatusRepository.Object,
                _toyRepository.Object,
                _mapper.Object
            );
        }

        [Fact]
        public async void Handle_ValidInput_ShouldSucceed()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                StatusId = 1,
                Amount = 75,
                Toys = new() { new() { ProductId = 1, Quantity = 5 }, new() { ProductId = 100, Quantity = 10 } }
            };

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.NotNull(result?.Data);
            Assert.Equal(201, result.Data);
            _orderStatusRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            _toyRepository.Verify(x => x.GetByIdsAsync(It.IsAny<List<int>>(), It.IsAny<CancellationToken>()), Times.Once);
            _mapper.Verify(x => x.Map<CreateOrderCommand, Order>(It.IsAny<CreateOrderCommand>()), Times.Once);
            _orderRepository.Verify(x => x.InsertAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void Handle_InvalidOrderStatus_ShouldFail()
        {
            // Arrange
            var command = new CreateOrderCommand { StatusId = 404 };
            var exceptionMessage = "Could not find status by the specified id";

            // Act
            Task action() => _handler.Handle(command, new CancellationToken());

            // Assert
            var exception = await Assert.ThrowsAsync<PSValidationException>(action);
            Assert.Single(exception.Errors);
            Assert.Equal(exceptionMessage, exception.Errors.First().ErrorMessage);
            _orderStatusRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            _toyRepository.Verify(x => x.GetByIdsAsync(It.IsAny<List<int>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async void Handle_InvalidProducts_ShouldFail()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                StatusId = 1,
                Toys = new() { new() { ProductId = 1 }, new() { ProductId = 404 } }
            };
            var exceptionMessage = $"Could not find some of the toys by the specified ids. Ids: {string.Join(", ", command.Toys.ConvertAll(x => x.ProductId))}";

            // Act
            Task action() => _handler.Handle(command, new CancellationToken());

            // Assert
            var exception = await Assert.ThrowsAsync<PSValidationException>(action);
            Assert.Single(exception.Errors);
            Assert.Equal(exceptionMessage, exception.Errors.First().ErrorMessage);
            _orderStatusRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            _toyRepository.Verify(x => x.GetByIdsAsync(It.IsAny<List<int>>(), It.IsAny<CancellationToken>()), Times.Once);
            _orderRepository.Verify(x => x.InsertAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async void Handle_InsufficientStock_ShouldFail()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                StatusId = 1,
                Toys = new() { new() { ProductId = 1, Quantity = 5 }, new() { ProductId = 100, Quantity = 11 } }
            };
            var exceptionMessage = $"There is not enough toys in the system. Id: 100. Requested: 11. Available: 10";

            // Act
            Task action() => _handler.Handle(command, new CancellationToken());

            // Assert
            var exception = await Assert.ThrowsAsync<PSValidationException>(action);
            Assert.Single(exception.Errors);
            Assert.Equal(exceptionMessage, exception.Errors.First().ErrorMessage);
            _orderStatusRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            _toyRepository.Verify(x => x.GetByIdsAsync(It.IsAny<List<int>>(), It.IsAny<CancellationToken>()), Times.Once);
            _orderRepository.Verify(x => x.InsertAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async void Handle_IncorrectAmount_ShouldFail()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                StatusId = 1,
                Amount = 80,
                Toys = new() { new() { ProductId = 1, Quantity = 5 }, new() { ProductId = 100, Quantity = 10 } }
            };
            var expectedAmount = command.Toys.Sum(x => x.Quantity * 5);
            var exceptionMessage = $"Amount is not correct. Amount: {command.Amount}. Expected amount: {expectedAmount}";

            // Act
            Task action() => _handler.Handle(command, new CancellationToken());

            // Assert
            var exception = await Assert.ThrowsAsync<PSValidationException>(action);
            Assert.Single(exception.Errors);
            Assert.Equal(exceptionMessage, exception.Errors.First().ErrorMessage);
            _orderStatusRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            _toyRepository.Verify(x => x.GetByIdsAsync(It.IsAny<List<int>>(), It.IsAny<CancellationToken>()), Times.Once);
            _orderRepository.Verify(x => x.InsertAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        private void SetupMocks()
        {
            _orderRepository = SetupOrderRepositoryMock();
            _orderStatusRepository = SetupOrderStatusRepositoryMock();
            _toyRepository = SetupToyRepositoryMock();
            _mapper = SetupMapperMock();
        }

        private static Mock<IOrderRepository> SetupOrderRepositoryMock()
        {
            Mock<IOrderRepository> mock = new();
            mock.Setup(x => x.InsertAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>())).ReturnsAsync(201);
            return mock;
        }

        private static Mock<IOrderStatusRepository> SetupOrderStatusRepositoryMock()
        {
            Mock<IOrderStatusRepository> mock = new();
            mock.Setup(x => x.GetByIdAsync(1, It.IsAny<bool>(), It.IsAny<CancellationToken>())).ReturnsAsync((int id, bool _, CancellationToken _) => new OrderStatus() { Id = id });
            mock.Setup(x => x.GetByIdAsync(404, It.IsAny<bool>(), It.IsAny<CancellationToken>())).ReturnsAsync((OrderStatus)null);
            return mock;
        }

        private static Mock<IToyRepository> SetupToyRepositoryMock()
        {
            Mock<IToyRepository> mock = new();
            mock.Setup(x => x.GetByIdsAsync(It.IsAny<List<int>>(), It.IsAny<CancellationToken>())).ReturnsAsync((List<int> ids, CancellationToken _) => ids.ConvertAll(id => new Toy { Id = id }));
            mock.Setup(x => x.GetByIdsAsync(It.Is<List<int>>(x => x.Contains(404)), It.IsAny<CancellationToken>())).ReturnsAsync((List<int> ids, CancellationToken _) => ids.Where(x => x != 404).Select(id => new Toy { Id = id }).ToList());
            mock.Setup(x => x.GetByIdsAsync(It.Is<List<int>>(x => x.Contains(100)), It.IsAny<CancellationToken>())).ReturnsAsync((List<int> ids, CancellationToken _) => ids.Where(x => x != 404).Select(id => new Toy { Id = id, Quantity = 10, Price = 5 }).ToList());
            return mock;
        }

        private static Mock<IMapper> SetupMapperMock()
        {
            Mock<IMapper> mock = new();
            mock.Setup(x => x.Map<CreateOrderCommand, Order>(It.IsAny<CreateOrderCommand>())).Returns(new Order());
            return mock;
        }
    }
}
