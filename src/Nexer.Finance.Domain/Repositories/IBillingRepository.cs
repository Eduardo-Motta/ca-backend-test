using Nexer.Finance.Domain.Entities;

namespace Nexer.Finance.Domain.Repositories
{
    public interface IBillingRepository
    {
        Task<BillingEntity?> FindBillingByInvoiceNumberAsync(string invoiceNumber, CancellationToken cancellationToken);
        Task CreateBillingAsync(BillingEntity billing, CancellationToken cancellationToken);
    }
}
