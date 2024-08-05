using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Products
{
    public interface IUpdateProductService
    {
        Task<Either<Error, bool>> UpdateProductAsync(Guid productId, ProductEntity product, CancellationToken cancellationToken);
    }
}
