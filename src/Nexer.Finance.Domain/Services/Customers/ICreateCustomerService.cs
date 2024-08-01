using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Customers
{
    public interface ICreateCustomerService
    {
        Task<Either<Error, int>> CreateCustomerAsync(CustomerEntity customer, CancellationToken cancellationToken);
    }
}
