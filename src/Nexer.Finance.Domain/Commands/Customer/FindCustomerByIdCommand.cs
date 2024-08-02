using Nexer.Finance.Shared.Commands;

namespace Nexer.Finance.Domain.Commands.Customer
{
    public class FindCustomerByIdCommand : ICommand
    {
        public int Id { get; set; }
    }
}
