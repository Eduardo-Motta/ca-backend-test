using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Products
{
    public class CreateProductService : ICreateProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public CreateProductService(ILogger<CreateProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<Either<Error, Guid>> CreateProductAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting service for product creation");

                var productFound = await _productRepository.FindProductByIdAsync(product.Id, cancellationToken);

                if (productFound is not null)
                {
                    _logger.LogInformation("Product with ID {Id} already exists", product.Id);

                    return Either<Error, Guid>.LeftValue(new Error("The provided ID already exists"));
                }

                await _productRepository.CreateProductAsync(product, cancellationToken);

                _logger.LogInformation("Product created sucessfullly with Id: {Id}", product.Id);

                return Either<Error, Guid>.RightValue(product.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating the product");
                return Either<Error, Guid>.LeftValue(new Error("An error occurred while creating the product"));
            }
        }
    }
}
