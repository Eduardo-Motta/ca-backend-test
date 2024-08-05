using Microsoft.Extensions.Logging;
using Moq;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Domain.Services.Customers;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.UnitTests.Nexer.Finance.Domain.Services.Customers
{
    public class CreateCustomerServiceTest
    {
        private readonly Mock<ILogger<CreateCustomerService>> _loggerMock;
        private readonly Mock<ICustomerRepository> _customerRespositoryMock;

        public CreateCustomerServiceTest()
        {
            _loggerMock = new Mock<ILogger<CreateCustomerService>>();
            _customerRespositoryMock = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async void ShouldCreateCustomer()
        {
            var entity = new CustomerEntity(name: "João Nascimento", email: "joao.nascimento@email.com", address: "Av. Brasil, 365 - Centro, São Paulo - SP");
            var cancellationToken = CancellationToken.None;

            _customerRespositoryMock.Setup(repository => repository.CreateCustomerAsync(entity, cancellationToken))
                .Returns(Task.CompletedTask);

            var service = new CreateCustomerService(_loggerMock.Object, _customerRespositoryMock.Object);

            var result = await service.CreateCustomerAsync(entity, cancellationToken);

            Assert.True(result.IsRight);
            Assert.IsType<Guid>(result.Right);
        }

        [Fact]
        public async void ShouldReturnErrorHandlingWhenNotCreateCustomer()
        {
            var entity = new CustomerEntity(name: "João Nascimento", email: "joao.nascimento@email.com", address: "Av. Brasil, 365 - Centro, São Paulo - SP");
            var cancellationToken = CancellationToken.None;

            _customerRespositoryMock
            .Setup(repo => repo.CreateCustomerAsync(It.IsAny<CustomerEntity>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Repository error"));

            var service = new CreateCustomerService(_loggerMock.Object, _customerRespositoryMock.Object);

            var result = await service.CreateCustomerAsync(entity, CancellationToken.None);

            Assert.True(result.IsLeft);
            Assert.IsType<Error>(result.Left);
            Assert.Equal("An error occurred while creating the customer", result.Left.Message);
        }
    }
}
