using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Commands;
using Nexer.Finance.Domain.Commands.Products;
using Nexer.Finance.Domain.Commands.Products.Validations;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Services.Products;
using Nexer.Finance.Shared.Commands;
using Nexer.Finance.Shared.Utils;
using Shared.Handlers;

namespace Nexer.Finance.Domain.Handlers.Products
{
    public class FindProductHandle : IHandler<FindProductByIdCommand>, IHandler<FindAllProductsCommand>
    {
        private readonly ILogger _logger;
        private readonly IFindProductService _findProductService;

        public FindProductHandle(ILogger<FindProductHandle> logger, IFindProductService findProductService)
        {
            _logger = logger;
            _findProductService = findProductService;
        }

        public async Task<ICommandResult> Handle(FindProductByIdCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting product search by id: {command}", command);

            _logger.LogInformation("Validating product command");
            var validate = await new FindProductByIdValidator().ValidateAsync(command, cancellationToken);

            if (validate.IsValid is false)
            {
                _logger.LogInformation("Product validated with errors: {Errors}", validate.Errors);
                return new CommandResponseErrors(validate.Errors);
            }

            var result = await _findProductService.FindProductByIdAsync(command.Id, cancellationToken);

            if (result.IsLeft)
            {
                return new CommandResponseError(result.Left.Message);
            }

            return new CommandResponseData<ProductEntity>(result.Right);
        }

        public async Task<ICommandResult> Handle(FindAllProductsCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting product search by id: {command}", command);

            _logger.LogInformation("Validating product command");
            var validate = await new FindAllProductsValidator().ValidateAsync(command, cancellationToken);

            if (validate.IsValid is false)
            {
                _logger.LogInformation("Product validated with errors: {Errors}", validate.Errors);
                return new CommandResponseErrors(validate.Errors);
            }

            _logger.LogInformation("Mapping pagination parameters");
            var pagination = new PaginationParameters(command.Pagination.PageNumber, command.Pagination.PageSize);

            var result = await _findProductService.FindAllProductsAsync(pagination, cancellationToken);

            if (result.IsLeft)
            {
                return new CommandResponseError(result.Left.Message);
            }

            return new CommandResponseData<IEnumerable<ProductEntity>>(result.Right);
        }
    }
}
