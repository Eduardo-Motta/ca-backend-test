using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Customers
{
    internal interface IUpdateCustomerService
    {
        Task<Either<Error, bool>> UpdateCustomerAsync(int customerId, CustomerEntity customer, CancellationToken cancellationToken);
    }
}
