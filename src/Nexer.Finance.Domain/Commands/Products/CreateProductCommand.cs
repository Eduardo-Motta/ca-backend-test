using Nexer.Finance.Shared.Commands;

namespace Nexer.Finance.Domain.Commands.Products
{
    public class CreateProductCommand : ICommand
    {
        public string Name { get; set; } = string.Empty;
    }
}
