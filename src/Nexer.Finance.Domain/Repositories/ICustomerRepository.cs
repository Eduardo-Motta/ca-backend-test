using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateCustomerAsync(CustomerEntity customer, CancellationToken cancellationToken);
        Task<CustomerEntity> FindCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken);
        Task<IEnumerable<CustomerEntity>> FindAllCustomersAsync(PaginationParameters paginationParameters, CancellationToken cancellationToken);
        Task UpdateCustomerAsync(CustomerEntity customer, CancellationToken cancellationToken);
    }
}
