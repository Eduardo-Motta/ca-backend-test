using Nexer.Finance.Shared.Commands;

namespace Nexer.Finance.Domain.Commands.Products
{
    public class FindAllProductsCommand : ICommand
    {
        public PaginationCommand Pagination { get; set; } = new PaginationCommand();
    }
}
