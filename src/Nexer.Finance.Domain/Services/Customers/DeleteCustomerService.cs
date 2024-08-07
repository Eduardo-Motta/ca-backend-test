using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Customers
{
    public class DeleteCustomerService : IDeleteCustomerService
    {
        private readonly ICustomerRepository _customerRespository;
        private readonly ILogger _logger;

        public DeleteCustomerService(ILogger<DeleteCustomerService> logger, ICustomerRepository customerRespository)
        {
            _logger = logger;
            _customerRespository = customerRespository;
        }

        public async Task<Either<Error, bool>> DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting service for delete customer");

                var customerFound = await _customerRespository.FindCustomerByIdAsync(customerId, cancellationToken);

                if (customerFound is null)
                {
                    _logger.LogInformation("Customer with ID not found: {customerId}", customerId);

                    return Either<Error, bool>.LeftValue(new Error("Not found"));
                }

                var hasLinkedBilling = await _customerRespository.CustomerHasLinkedBilling(customerId, cancellationToken);
                if (hasLinkedBilling is true)
                {
                    _logger.LogInformation("It is not possible to delete a customer with linked billing: {customerId}", customerId);

                    return Either<Error, bool>.LeftValue(new Error("It is not possible to delete a customer with linked billing"));
                }

                await _customerRespository.DeleteCustomerAsync(customerFound, cancellationToken);

                _logger.LogInformation("Customer deleted sucessfullly with Id: {customerId}", customerId);

                return Either<Error, bool>.RightValue(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting the customer");
                return Either<Error, bool>.LeftValue(new Error("An error occurred while deleting the customer"));
            }
        }
    }
}
