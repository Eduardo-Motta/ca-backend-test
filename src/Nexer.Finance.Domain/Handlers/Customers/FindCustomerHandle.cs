using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Commands;
using Nexer.Finance.Domain.Commands.Customer;
using Nexer.Finance.Domain.Commands.Customer.Validations;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Services.Customers;
using Nexer.Finance.Shared.Commands;
using Nexer.Finance.Shared.Utils;
using Shared.Handlers;

namespace Nexer.Finance.Domain.Handlers.Customers
{
    public class FindCustomerHandle : IHandler<FindCustomerByIdCommand>, IHandler<FindAllCustomersCommand>
    {
        private readonly ILogger _logger;
        private readonly IFindCustomerService _findCustomerService;

        public FindCustomerHandle(ILogger<CreateCustomerHandle> logger, IFindCustomerService findCustomerService)
        {
            _logger = logger;
            _findCustomerService = findCustomerService;
        }

        public async Task<ICommandResult> Handle(FindCustomerByIdCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting customer search by id: {command}", command);

            _logger.LogInformation("Validating customer command");
            var validate = await new FindCustomerByIdValidator().ValidateAsync(command, cancellationToken);

            if (validate.IsValid is false)
            {
                _logger.LogInformation("Customer validated with errors: {Errors}", validate.Errors);
                return new CommandResponseErrors(validate.Errors);
            }

            var result = await _findCustomerService.FindCustomerByIdAsync(command.Id, cancellationToken);

            if (result.IsLeft)
            {
                return new CommandResponseError(result.Left.Message);
            }

            return new CommandResponseData<CustomerEntity>(result.Right);
        }

        public async Task<ICommandResult> Handle(FindAllCustomersCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting customer search by id: {command}", command);

            _logger.LogInformation("Validating customer command");
            var validate = await new FindAllCustomersValidator().ValidateAsync(command, cancellationToken);

            if (validate.IsValid is false)
            {
                _logger.LogInformation("Customer validated with errors: {Errors}", validate.Errors);
                return new CommandResponseErrors(validate.Errors);
            }

            _logger.LogInformation("Mapping pagination parameters");
            var pagination = new PaginationParameters(command.Pagination.PageNumber, command.Pagination.PageSize);

            var result = await _findCustomerService.FindAllCustomersAsync(pagination, cancellationToken);

            if (result.IsLeft)
            {
                return new CommandResponseError(result.Left.Message);
            }

            return new CommandResponseData<IEnumerable<CustomerEntity>>(result.Right);
        }
    }
}
