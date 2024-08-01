using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Commands;
using Nexer.Finance.Domain.Commands.Customer;
using Nexer.Finance.Domain.Commands.Customer.Validations;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Services.Customers;
using Nexer.Finance.Shared.Commands;
using Shared.Handlers;

namespace Nexer.Finance.Domain.Handlers
{
    public class CreateCustomerHandle : IHandler<CreateCustomerCommand>
    {
        private readonly ILogger _logger;
        private readonly ICreateCustomerService _createCustomerService;

        public CreateCustomerHandle(ILogger<CreateCustomerHandle> logger, ICreateCustomerService createCustomerService)
        {
            _logger = logger;
            _createCustomerService = createCustomerService;
        }

        public async Task<ICommandResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting customer creation: {command}", command);

            _logger.LogInformation("Validating customer data");
            var validate = await new CreateCustomerValidator().ValidateAsync(command, cancellationToken);

            if (validate.IsValid is false)
            {
                _logger.LogInformation("Customer validated with errors: {Errors}", validate.Errors);
                return new CommandResponseErrors(validate.Errors);
            }

            _logger.LogInformation("Mapping customer entity");
            var customer = new CustomerEntity(command.Name, command.Email, command.Address);

            var result = await _createCustomerService.CreateCustomerAsync(customer, cancellationToken);

            if (result.IsLeft)
            {
                return new CommandResponseError(result.Left.Message);
            }

            return new CommandResponseData<int>(result.Right);
        }
    }
}
