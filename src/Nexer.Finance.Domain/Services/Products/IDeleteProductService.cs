using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Products
{
    public interface IDeleteProductService
    {
        Task<Either<Error, bool>> DeleteProductAsync(Guid productId, CancellationToken cancellationToken);
    }
}
