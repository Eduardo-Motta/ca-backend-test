using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Customers
{
    public class CreateCustomerService : ICreateCustomerService
    {
        private readonly ICustomerRepository _customerRespository;
        private readonly ILogger _logger;

        public CreateCustomerService(ILogger<CreateCustomerService> logger, ICustomerRepository customerRespository)
        {
            _logger = logger;
            _customerRespository = customerRespository;
        }

        public async Task<Either<Error, Guid>> CreateCustomerAsync(CustomerEntity customer, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting service for customer creation");

                var customerFound = await _customerRespository.FindCustomerByIdAsync(customer.Id, cancellationToken);

                if (customerFound is not null)
                {
                    _logger.LogInformation("Customer with ID {Id} already exists", customer.Id);

                    return Either<Error, Guid>.LeftValue(new Error("The provided ID already exists"));
                }

                await _customerRespository.CreateCustomerAsync(customer, cancellationToken);

                _logger.LogInformation("Customer created sucessfullly with Id: {Id}", customer.Id);

                return Either<Error, Guid>.RightValue(customer.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating the customer");
                return Either<Error, Guid>.LeftValue(new Error("An error occurred while creating the customer"));
            }
        }
    }
}
