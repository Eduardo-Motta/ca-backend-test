using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Products
{
    public interface IFindProductService
    {
        Task<Either<Error, ProductEntity>> FindProductByIdAsync(Guid productId, CancellationToken cancellationToken);
        Task<Either<Error, IEnumerable<ProductEntity>>> FindAllProductsAsync(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    }
}
