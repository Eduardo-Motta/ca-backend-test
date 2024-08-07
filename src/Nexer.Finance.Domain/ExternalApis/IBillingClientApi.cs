using Nexer.Finance.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexer.Finance.Domain.ExternalApis
{
    public interface IBillingClientApi
    {
        Task<List<BillingDto>> GetBillingAsync(CancellationToken cancellationToken);
    }
}
