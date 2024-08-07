namespace Nexer.Finance.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; protected set; }
    }
}
