using System.Xml.Linq;

namespace Nexer.Finance.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        public ProductEntity(string name)
        {
            Name = name;
        }

        public string Name { get; private set; } = string.Empty;

        public void Update(string name)
        {
            Name = name;
        }
    }
}
