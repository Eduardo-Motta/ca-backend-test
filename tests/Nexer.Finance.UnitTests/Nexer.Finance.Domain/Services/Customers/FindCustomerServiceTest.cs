using Microsoft.Extensions.Logging;
using Moq;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Domain.Services.Customers;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.UnitTests.Nexer.Finance.Domain.Services.Customers
{
    public class FindCustomerServiceTest
    {
        private readonly Mock<ILogger<FindCustomerService>> _loggerMock;
        private readonly Mock<ICustomerRepository> _customerRespositoryMock;

        public FindCustomerServiceTest()
        {
            _loggerMock = new Mock<ILogger<FindCustomerService>>();
            _customerRespositoryMock = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async void ShouldReturnCustomerByValidId()
        {
            var entity = new CustomerEntity(name: "Carlos Araujo", email: "carlos.ar@gmail.com", address: "Av. Plácido de Castro, 35 D - Centro, São Paulo - SP");
            var cancellationToken = CancellationToken.None;
            var customerId = Guid.NewGuid();

            _customerRespositoryMock.Setup(repository => repository.FindCustomerByIdAsync(customerId, cancellationToken))
            .ReturnsAsync(entity);

            var service = new FindCustomerService(_loggerMock.Object, _customerRespositoryMock.Object);

            var result = await service.FindCustomerByIdAsync(customerId, cancellationToken);

            Assert.True(result.IsRight);
            Assert.IsType<Guid>(result.Right.Id);
            Assert.Equal(entity.Name, result.Right.Name);
        }

        [Fact]
        public async void ShouldReturnNullWhenCustomerNotfoundById()
        {
            var cancellationToken = CancellationToken.None;
            var customerId = Guid.NewGuid();

            _customerRespositoryMock.Setup(repository => repository.FindCustomerByIdAsync(customerId, cancellationToken))
            .ReturnsAsync((CustomerEntity)null!);

            var service = new FindCustomerService(_loggerMock.Object, _customerRespositoryMock.Object);

            var result = await service.FindCustomerByIdAsync(customerId, cancellationToken);

            Assert.True(result.IsLeft);
            Assert.Equal("Not found", result.Left.Message);
        }

        [Fact]
        public async void ShouldReturnCustomerList()
        {
            var customers = new List<CustomerEntity>
            {
                new CustomerEntity(name: "Joana Martins", email: "j.martins@gmail.com", address: "Av. Mato Grosso, 2103 - Centro, São Paulo - SP"),
                new CustomerEntity(name: "Carolina Araujo", email: "araujocarol@gmail.com", address: "Av. Figueiredo, 6354 - Centro, São Paulo - SP"),
                new CustomerEntity(name: "Roni Piper", email: "piper@gmail.com", address: "Av. Plácido de Castro, 687 - Centro, São Paulo - SP"),
                new CustomerEntity(name: "Claudio Gonçalves", email: "cg@gmail.com", address: "Av. Maracanã, 111 - Centro, Rio de Janeiro - RJ"),
                new CustomerEntity(name: "Matheus Lima", email: "mat.lima@gmail.com", address: "Av. Brasil, 6846 - Centro, Ariquemes - RO"),
            };
            var pagination = new PaginationParameters(1, 5);
            var cancellationToken = CancellationToken.None;

            _customerRespositoryMock.Setup(repository => repository.FindAllCustomersAsync(pagination, cancellationToken))
            .ReturnsAsync(customers);

            var service = new FindCustomerService(_loggerMock.Object, _customerRespositoryMock.Object);

            var result = await service.FindAllCustomersAsync(pagination, cancellationToken);

            Assert.True(result.IsRight);
            Assert.Equal(5, result.Right.Count());
        }

        [Fact]
        public async void ShouldReturnCustomerEmptyList()
        {
            var customers = new List<CustomerEntity>();
            var pagination = new PaginationParameters(1, 10);
            var cancellationToken = CancellationToken.None;

            _customerRespositoryMock.Setup(repository => repository.FindAllCustomersAsync(pagination, cancellationToken))
            .ReturnsAsync(customers);

            var service = new FindCustomerService(_loggerMock.Object, _customerRespositoryMock.Object);

            var result = await service.FindAllCustomersAsync(pagination, cancellationToken);

            Assert.True(result.IsRight);
            Assert.Empty(result.Right);
        }

        [Fact]
        public async void ShouldReturnErrorHandlingWhenAnExceptionOccursOnFindCustomerById()
        {
            var customerId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;

            _customerRespositoryMock.Setup(repository => repository.FindCustomerByIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ThrowsAsync(new Exception("Repository error"));

            var service = new FindCustomerService(_loggerMock.Object, _customerRespositoryMock.Object);

            var result = await service.FindCustomerByIdAsync(customerId, cancellationToken);

            Assert.True(result.IsLeft);
            Assert.IsType<Error>(result.Left);
            Assert.Equal("An error occurred while searching for the customer", result.Left.Message);
        }

        [Fact]
        public async void ShouldReturnErrorHandlingWhenAnExceptionOccursOnFindByIdFindAllCustomers()
        {
            var pagination = new PaginationParameters(2, 20);
            var cancellationToken = CancellationToken.None;

            _customerRespositoryMock.Setup(repository => repository.FindAllCustomersAsync(pagination, cancellationToken))
            .ThrowsAsync(new Exception("Repository error"));

            var service = new FindCustomerService(_loggerMock.Object, _customerRespositoryMock.Object);

            var result = await service.FindAllCustomersAsync(pagination, cancellationToken);

            Assert.True(result.IsLeft);
            Assert.IsType<Error>(result.Left);
            Assert.Equal("An error occurred while searching for the customers", result.Left.Message);
        }
    }
}
