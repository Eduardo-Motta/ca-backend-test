using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Commands;
using Nexer.Finance.Domain.Commands.Products;
using Nexer.Finance.Domain.Services.Products;
using Nexer.Finance.Shared.Commands;
using Shared.Handlers;

namespace Nexer.Finance.Domain.Handlers.Products
{
    public class DeleteProductHandle : IHandler<DeleteProductCommand>
    {
        private readonly ILogger _logger;
        private readonly IDeleteProductService _deleteProductService;

        public DeleteProductHandle(ILogger<DeleteProductHandle> logger, IDeleteProductService deleteProductService)
        {
            _logger = logger;
            _deleteProductService = deleteProductService;
        }

        public async Task<ICommandResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting product deletion: {command}", command);

            var result = await _deleteProductService.DeleteProductAsync(command.Id, cancellationToken);

            if (result.IsLeft)
            {
                return new CommandResponseError(result.Left.Message);
            }

            return new CommandResponseData<bool>(result.Right);
        }
    }
}
