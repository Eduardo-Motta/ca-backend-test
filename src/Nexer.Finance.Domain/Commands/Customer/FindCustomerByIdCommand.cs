using Nexer.Finance.Shared.Commands;

namespace Nexer.Finance.Domain.Commands.Customer
{
    public class FindCustomerByIdCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
