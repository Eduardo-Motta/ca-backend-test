namespace Nexer.Finance.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        public ProductEntity() { }

        public ProductEntity(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set; } = string.Empty;

        public void Update(string name)
        {
            Name = name;
        }
    }
}
