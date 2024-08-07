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
    public class CreateProductHandle : IHandler<CreateProductCommand>
    {
        private readonly ILogger _logger;
        private readonly ICreateProductService _createProductService;

        public CreateProductHandle(ILogger<CreateProductHandle> logger, ICreateProductService createProductService)
        {
            _logger = logger;
            _createProductService = createProductService;
        }

        public async Task<ICommandResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting product creation: {command}", command);

            _logger.LogInformation("Validating product data");
            var validate = await new CreateProductValidator().ValidateAsync(command, cancellationToken);

            if (validate.IsValid is false)
            {
                _logger.LogInformation("Product validated with errors: {Errors}", validate.Errors);
                return new CommandResponseErrors(validate.Errors);
            }

            _logger.LogInformation("Mapping product entity");
            var product = new ProductEntity(command.Id, command.Name);

            var result = await _createProductService.CreateProductAsync(product, cancellationToken);

            if (result.IsLeft)
            {
                return new CommandResponseError(result.Left.Message);
            }

            return new CommandResponseData<Guid>(result.Right);
        }
    }
}
