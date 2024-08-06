using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Products
{
    public interface ICreateProductService
    {
        Task<Either<Error, Guid>> CreateProductAsync(ProductEntity product, CancellationToken cancellationToken);
    }
}
