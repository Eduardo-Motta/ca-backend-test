using Microsoft.Extensions.Logging;
using Moq;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Domain.Services.Customers;

namespace Nexer.Finance.UnitTests.Nexer.Finance.Domain.Services.Customers
{
    public class UpdateCustomerServiceTest
    {
        private readonly Mock<ILogger<UpdateCustomerService>> _loggerMock;
        private readonly Mock<ICustomerRepository> _customerRespositoryMock;

        public UpdateCustomerServiceTest()
        {
            _loggerMock = new Mock<ILogger<UpdateCustomerService>>();
            _customerRespositoryMock = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async void ShouldUpdateCustomer()
        {
            var customerId = Guid.NewGuid();
            var entity = new CustomerEntity(Guid.NewGuid(), name: "João Nascimento", email: "joao.nascimento@email.com", address: "Av. Brasil, 365 - Centro, São Paulo - SP");
            var cancellationToken = CancellationToken.None;

            _customerRespositoryMock.Setup(repository => repository.FindCustomerByIdAsync(customerId, cancellationToken))
                .ReturnsAsync(entity);

            _customerRespositoryMock.Setup(repository => repository.UpdateCustomerAsync(entity, cancellationToken))
                .Returns(Task.CompletedTask);

            var service = new UpdateCustomerService(_loggerMock.Object, _customerRespositoryMock.Object);

            var result = await service.UpdateCustomerAsync(customerId, entity, cancellationToken);

            Assert.True(result.IsRight);
            Assert.IsType<bool>(result.Right);
        }

        [Fact]
        public async void ShouldNotUpdateWhenCustomerIsNotFound()
        {
            var customerId = Guid.NewGuid();
            var entity = new CustomerEntity(Guid.NewGuid(), name: "Adriano Borges", email: "borges.adriano@email.com", address: "Av. Brasil, 365 - Centro, São Paulo - SP");
            var cancellationToken = CancellationToken.None;

            _customerRespositoryMock.Setup(repository => repository.FindCustomerByIdAsync(customerId, cancellationToken))
                .ReturnsAsync((CustomerEntity)null!);

            var service = new UpdateCustomerService(_loggerMock.Object, _customerRespositoryMock.Object);

            var result = await service.UpdateCustomerAsync(customerId, entity, cancellationToken);

            Assert.True(result.IsLeft);
            Assert.Equal("Not found", result.Left.Message);
        }

        [Fact]
        public async void ShouldReturnErrorHandlingWhenNotCreateCustomer()
        {
            var customerId = Guid.NewGuid();
            var entity = new CustomerEntity(Guid.NewGuid(), name: "Aline Junior", email: "al.junior@email.com", address: "Av. Brasil, 365 - Centro, São Paulo - SP");
            var cancellationToken = CancellationToken.None;

            _customerRespositoryMock.Setup(repository => repository.FindCustomerByIdAsync(customerId, cancellationToken))
                .ReturnsAsync(entity);

            _customerRespositoryMock.Setup(repository => repository.UpdateCustomerAsync(It.IsAny<CustomerEntity>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Repository error"));

            var service = new UpdateCustomerService(_loggerMock.Object, _customerRespositoryMock.Object);

            var result = await service.UpdateCustomerAsync(customerId, entity, cancellationToken);

            Assert.True(result.IsLeft);
            Assert.Equal("An error occurred while updating the customer", result.Left.Message);
        }
    }
}
