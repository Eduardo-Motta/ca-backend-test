using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Customers
{
    public class FindCustomerService : IFindCustomerService
    {
        private readonly ICustomerRepository _customerRespository;
        private readonly ILogger _logger;

        public FindCustomerService(ILogger<FindCustomerService> logger, ICustomerRepository customerRespository)
        {
            _logger = logger;
            _customerRespository = customerRespository;
        }

        public async Task<Either<Error, CustomerEntity>> FindCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting service to search for customers by ID");

                var customer = await _customerRespository.FindCustomerByIdAsync(customerId, cancellationToken);

                if (customer is null)
                {
                    _logger.LogInformation("Customer with ID not found: {customerId}", customerId);

                    return Either<Error, CustomerEntity>.LeftValue(new Error("Customer not found"));
                }

                _logger.LogInformation("Customer with ID found successfully");

                return Either<Error, CustomerEntity>.RightValue(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while searching for the customer");
                return Either<Error, CustomerEntity>.LeftValue(new Error("An error occurred while searching for the customer"));
            }
        }

        public async Task<Either<Error, IEnumerable<CustomerEntity>>> FindAllCustomersAsync(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting service to search for customers");

                var customers = await _customerRespository.FindAllCustomersAsync(paginationParameters, cancellationToken);

                _logger.LogInformation("Returning incoming customers");

                return Either<Error, IEnumerable<CustomerEntity>>.RightValue(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while searching for the customers");
                return Either<Error, IEnumerable<CustomerEntity>>.LeftValue(new Error("An error occurred while searching for the customers"));
            }
        }
    }
}
