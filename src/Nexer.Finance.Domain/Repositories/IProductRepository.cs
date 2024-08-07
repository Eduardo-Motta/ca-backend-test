using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Repositories
{
    public interface IProductRepository
    {
        Task CreateProductAsync(ProductEntity product, CancellationToken cancellationToken);
        Task<ProductEntity?> FindProductByIdAsync(Guid productId, CancellationToken cancellationToken);
        Task<IEnumerable<ProductEntity>> FindAllProductsAsync(PaginationParameters paginationParameters, CancellationToken cancellationToken);
        Task UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken);
        Task DeleteProductAsync(ProductEntity product, CancellationToken cancellationToken);
        Task<bool> ProductHasLinkedBilling(Guid productId, CancellationToken cancellationToken);
    }
}
