using Nexer.Finance.Domain.Dtos;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Billings
{
    public interface IImportBillingService
    {
        Task<Either<Error, BillingImportStatusDto>> ImportaAll(CancellationToken cancellationToken);
    }
}
