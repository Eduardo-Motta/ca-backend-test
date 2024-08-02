using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Customers
{
    internal class UpdateCustomerService : IUpdateCustomerService
    {
        private readonly ICustomerRepository _customerRespository;
        private readonly ILogger _logger;

        public UpdateCustomerService(ILogger<CreateCustomerService> logger, ICustomerRepository customerRespository)
        {
            _logger = logger;
            _customerRespository = customerRespository;
        }

        public async Task<Either<Error, bool>> UpdateCustomerAsync(int customerId, CustomerEntity customer, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting service for update customer");

                var customerFound = await _customerRespository.FindCustomerByIdAsync(customerId, cancellationToken);

                if (customerFound is null)
                {
                    _logger.LogInformation("Customer with ID not found: {customerId}", customerId);

                    return Either<Error, bool>.LeftValue(new Error("Customer not found"));
                }

                _logger.LogInformation("Mapping customer entity");
                customerFound.Name = customer.Name;
                customerFound.Email = customer.Email;
                customerFound.Address = customer.Address;

                await _customerRespository.UpdateCustomerAsync(customerFound, cancellationToken);

                _logger.LogInformation("Customer updated sucessfullly with Id: {Id}", customer.Id);

                return Either<Error, bool>.RightValue(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating the customer");
                return Either<Error, bool>.LeftValue(new Error("An error occurred while updating the customer"));
            }
        }
    }
}
