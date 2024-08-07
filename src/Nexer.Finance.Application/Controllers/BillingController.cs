using Microsoft.AspNetCore.Mvc;
using Nexer.Finance.Domain.Services.Billings;

namespace Nexer.Finance.Application.Controllers
{
    public class BillingController : BaseController
    {
        public BillingController(ILogger<CustomerController> logger) { }

        [HttpPost(("importBillings"))]
        public async Task<IActionResult> ImportBilling([FromServices] IImportBillingService service, CancellationToken cancellationToken)
        {
            var result = await service.ImportaAll(cancellationToken);

            if (result.IsLeft)
            {
                return HandleResponse(result.Left);
            }

            return HandleResponse(result.Right);
        }
    }
}
