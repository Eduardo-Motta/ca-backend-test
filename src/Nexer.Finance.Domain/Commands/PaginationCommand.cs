using Nexer.Finance.Shared.Commands;

namespace Nexer.Finance.Domain.Commands
{
    public class PaginationCommand : ICommand
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
