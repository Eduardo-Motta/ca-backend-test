using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Commands;
using Nexer.Finance.Domain.Commands.Customer;
using Nexer.Finance.Domain.Services.Customers;
using Nexer.Finance.Shared.Commands;
using Shared.Handlers;

namespace Nexer.Finance.Domain.Handlers.Customers
{
    public class DeleteCustomerHandle : IHandler<DeleteCustomerCommand>
    {
        private readonly ILogger _logger;
        private readonly IDeleteCustomerService _deleteCustomerService;

        public DeleteCustomerHandle(ILogger<DeleteCustomerHandle> logger, IDeleteCustomerService deleteCustomerService)
        {
            _logger = logger;
            _deleteCustomerService = deleteCustomerService;
        }

        public async Task<ICommandResult> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting customer deletion: {command}", command);

            var result = await _deleteCustomerService.DeleteCustomerAsync(command.Id, cancellationToken);

            if (result.IsLeft)
            {
                return new CommandResponseError(result.Left.Message);
            }

            return new CommandResponseData<bool>(result.Right);
        }
    }
}
