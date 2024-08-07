using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Commands;
using Nexer.Finance.Domain.Commands.Customer;
using Nexer.Finance.Domain.Commands.Customer.Validations;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Services.Customers;
using Nexer.Finance.Shared.Commands;
using Shared.Handlers;

namespace Nexer.Finance.Domain.Handlers.Customers
{
    public class UpdateCustomerHandle : IHandler<UpdateCustomerCommand>
    {
        private readonly ILogger _logger;
        private readonly IUpdateCustomerService _updateCustomerService;

        public UpdateCustomerHandle(ILogger<UpdateCustomerHandle> logger, IUpdateCustomerService updateCustomerService)
        {
            _logger = logger;
            _updateCustomerService = updateCustomerService;
        }

        public async Task<ICommandResult> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting update customer: {command}", command);

            _logger.LogInformation("Validating customer command");
            var validate = await new UpdateCustomerValidator().ValidateAsync(command, cancellationToken);

            if (validate.IsValid is false)
            {
                _logger.LogInformation("Customer validated with errors: {Errors}", validate.Errors);
                return new CommandResponseErrors(validate.Errors);
            }

            _logger.LogInformation("Mapping customer entity");
            var customer = new CustomerEntity(command.Id, command.Name, command.Email, command.Address);

            var result = await _updateCustomerService.UpdateCustomerAsync(command.Id, customer, cancellationToken);

            if (result.IsLeft)
            {
                return new CommandResponseError(result.Left.Message);
            }

            return new CommandResponseData<bool>(result.Right);
        }
    }
}
