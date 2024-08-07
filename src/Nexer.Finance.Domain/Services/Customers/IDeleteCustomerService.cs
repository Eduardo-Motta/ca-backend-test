using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Customers
{
    public interface IDeleteCustomerService
    {
        Task<Either<Error, bool>> DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken);
    }
}
