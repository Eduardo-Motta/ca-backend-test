using Nexer.Finance.Shared.Commands;

namespace Nexer.Finance.Domain.Commands.Products
{
    public class FindProductByIdCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
