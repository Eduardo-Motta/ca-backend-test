using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Customers
{
    public interface IFindCustomerService
    {
        Task<Either<Error, CustomerEntity>> FindCustomerByIdAsync(int customerId, CancellationToken cancellationToken);
        Task<Either<Error, IEnumerable<CustomerEntity>>> FindAllCustomersAsync(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    }
}
