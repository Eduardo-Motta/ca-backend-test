using Microsoft.EntityFrameworkCore;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Infrastructure.Context;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;
        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            await _context.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProductAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProductEntity>> FindAllProductsAsync(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            int skipCount = (paginationParameters.PageNumber - 1) * paginationParameters.PageSize;

            return await _context.Products
                .Skip(skipCount)
                .Take(paginationParameters.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<ProductEntity?> FindProductByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            return await _context.Products.Where(x => x.Id == productId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> ProductHasLinkedBilling(Guid productId, CancellationToken cancellationToken)
        {
            return await _context.BillingLines.Where(x => x.ProductId == productId).AnyAsync(cancellationToken);
        }

        public async Task UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
