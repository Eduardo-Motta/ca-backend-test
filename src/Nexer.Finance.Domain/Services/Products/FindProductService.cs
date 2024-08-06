using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Products
{
    public class FindProductService : IFindProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public FindProductService(ILogger<FindProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<Either<Error, IEnumerable<ProductEntity>>> FindAllProductsAsync(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting service to search for products");

                var products = await _productRepository.FindAllProductsAsync(paginationParameters, cancellationToken);

                _logger.LogInformation("Returning incoming products");

                return Either<Error, IEnumerable<ProductEntity>>.RightValue(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while searching for the products");
                return Either<Error, IEnumerable<ProductEntity>>.LeftValue(new Error("An error occurred while searching for the products"));
            }
        }

        public async Task<Either<Error, ProductEntity>> FindProductByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting service to search for customers by ID");

                var product = await _productRepository.FindProductByIdAsync(productId, cancellationToken);

                if (product is null)
                {
                    _logger.LogInformation("Product with ID not found: {productId}", productId);

                    return Either<Error, ProductEntity>.LeftValue(new Error("Product not found"));
                }

                _logger.LogInformation("Product with ID found successfully");

                return Either<Error, ProductEntity>.RightValue(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while searching for the product");
                return Either<Error, ProductEntity>.LeftValue(new Error("An error occurred while searching for the product"));
            }
        }
    }
}
