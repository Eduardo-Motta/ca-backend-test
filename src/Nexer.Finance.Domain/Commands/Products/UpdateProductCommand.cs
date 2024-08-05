using Nexer.Finance.Shared.Commands;

namespace Nexer.Finance.Domain.Commands.Products
{
    public class UpdateProductCommand : ICommand
    {
        public Guid Id { get; private set; }
        public string Name { get; set; } = string.Empty;

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
