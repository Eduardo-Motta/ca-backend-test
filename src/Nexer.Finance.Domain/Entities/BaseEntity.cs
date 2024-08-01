namespace Nexer.Finance.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; protected set; }
    }
}
