namespace Nexer.Finance.Domain.Entities
{
    public class CustomerEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public CustomerEntity(Guid id, string name, string email, string address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }
    }
}
