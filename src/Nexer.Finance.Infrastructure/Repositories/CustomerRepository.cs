using Microsoft.EntityFrameworkCore;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Infrastructure.Context;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _context;
        public CustomerRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateCustomerAsync(CustomerEntity customer, CancellationToken cancellationToken)
        {
            await _context.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<CustomerEntity>> FindAllCustomersAsync(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            int skipCount = (paginationParameters.PageNumber - 1) * paginationParameters.PageSize;

            return await _context.Costomers
                .Skip(skipCount)
                .Take(paginationParameters.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<CustomerEntity?> FindCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken)
        {
            return await _context.Costomers.Where(x => x.Id == customerId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateCustomerAsync(CustomerEntity customer, CancellationToken cancellationToken)
        {
            _context.Costomers.Update(customer);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
