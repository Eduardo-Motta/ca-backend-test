using Nexer.Finance.Domain.Entities;

namespace Nexer.Finance.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateCustomerAsync(CustomerEntity customer, CancellationToken cancellationToken);
    }
}
