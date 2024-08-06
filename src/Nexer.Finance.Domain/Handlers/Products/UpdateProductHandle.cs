using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Commands;
using Nexer.Finance.Domain.Commands.Products;
using Nexer.Finance.Domain.Commands.Products.Validations;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Services.Products;
using Nexer.Finance.Shared.Commands;
using Shared.Handlers;

namespace Nexer.Finance.Domain.Handlers.Products
{
    public class UpdateProductHandle : IHandler<UpdateProductCommand>
    {
        private readonly ILogger _logger;
        private readonly IUpdateProductService _updateProductService;

        public UpdateProductHandle(ILogger<UpdateProductHandle> logger, IUpdateProductService updateProductService)
        {
            _logger = logger;
            _updateProductService = updateProductService;
        }

        public async Task<ICommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting update product: {command}", command);

            _logger.LogInformation("Validating product command");
            var validate = await new UpdateProductValidator().ValidateAsync(command, cancellationToken);

            if (validate.IsValid is false)
            {
                _logger.LogInformation("Product validated with errors: {Errors}", validate.Errors);
                return new CommandResponseErrors(validate.Errors);
            }

            _logger.LogInformation("Mapping product entity");
            var product = new ProductEntity(command.Name);

            var result = await _updateProductService.UpdateProductAsync(command.Id, product, cancellationToken);

            if (result.IsLeft)
            {
                return new CommandResponseError(result.Left.Message);
            }

            return new CommandResponseData<bool>(result.Right);
        }
    }
}
