using Nexer.Finance.Shared.Commands;

namespace Nexer.Finance.Domain.Commands.Customer
{
    public class FindAllCustomersCommand : ICommand
    {
        public PaginationCommand Pagination { get; set; } = new PaginationCommand();
    }
}
