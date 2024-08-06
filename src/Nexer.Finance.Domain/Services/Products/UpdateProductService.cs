using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Products
{
    public class UpdateProductService : IUpdateProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public UpdateProductService(ILogger<UpdateProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<Either<Error, bool>> UpdateProductAsync(Guid productId, ProductEntity product, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting service for update product");

                var productFound = await _productRepository.FindProductByIdAsync(productId, cancellationToken);

                if (productFound is null)
                {
                    _logger.LogInformation("Product with ID not found: {productId}", product);

                    return Either<Error, bool>.LeftValue(new Error("Product not found"));
                }

                _logger.LogInformation("Mapping product entity");
                productFound.Update(product.Name);

                await _productRepository.UpdateProductAsync(productFound, cancellationToken);

                _logger.LogInformation("Product updated sucessfullly with Id: {Id}", product.Id);

                return Either<Error, bool>.RightValue(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating the product");
                return Either<Error, bool>.LeftValue(new Error("An error occurred while updating the product"));
            }
        }
    }
}
