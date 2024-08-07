using Microsoft.EntityFrameworkCore;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Infrastructure.Context;

namespace Nexer.Finance.Infrastructure.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly DatabaseContext _context;
        public BillingRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateBillingAsync(BillingEntity billing, CancellationToken cancellationToken)
        {
            await _context.Billings.AddAsync(billing, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<BillingEntity?> FindBillingByInvoiceNumberAsync(string invoiceNumber, CancellationToken cancellationToken)
        {
            return await _context.Billings.Where(x => x.InvoiceNumber == invoiceNumber).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
