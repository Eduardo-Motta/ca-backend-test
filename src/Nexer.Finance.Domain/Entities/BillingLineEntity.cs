namespace Nexer.Finance.Domain.Entities
{
    public class BillingLineEntity : BaseEntity
    {
        public BillingLineEntity()
        {
            Id = Guid.NewGuid();
            Product = null!;
        }

        public BillingLineEntity(Guid billingId, Guid productId, int quantity, decimal unitPrice, decimal subtotal)
        {
            Id = Guid.NewGuid();
            BillingId = billingId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Subtotal = subtotal;
            Product = null!;
        }

        public Guid BillingId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Subtotal { get; private set; }
        public ProductEntity Product { get; private set; }
    }
}
