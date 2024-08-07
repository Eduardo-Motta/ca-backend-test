using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Products
{
    public class DeleteProductService : IDeleteProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public DeleteProductService(ILogger<DeleteProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<Either<Error, bool>> DeleteProductAsync(Guid productId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting service for delete product");

                var productFound = await _productRepository.FindProductByIdAsync(productId, cancellationToken);

                if (productFound is null)
                {
                    _logger.LogInformation("Product with ID not found: {productId}", productId);

                    return Either<Error, bool>.LeftValue(new Error("Product not found"));
                }

                var hasLinkedBilling = await _productRepository.ProductHasLinkedBilling(productId, cancellationToken);
                if (hasLinkedBilling is true)
                {
                    _logger.LogInformation("It is not possible to delete a product with linked billing: {productId}", productId);

                    return Either<Error, bool>.LeftValue(new Error("It is not possible to delete a product with linked billing"));
                }

                await _productRepository.DeleteProductAsync(productFound, cancellationToken);

                _logger.LogInformation("Product deleted sucessfullly with Id: {productId}", productId);

                return Either<Error, bool>.RightValue(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting the product");
                return Either<Error, bool>.LeftValue(new Error("An error occurred while deleting the product"));
            }
        }
    }
}
