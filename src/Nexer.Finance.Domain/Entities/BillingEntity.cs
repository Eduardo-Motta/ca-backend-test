namespace Nexer.Finance.Domain.Entities
{
    public class BillingEntity : BaseEntity
    {
        public BillingEntity(string invoiceNumber, Guid customerId, DateTime date, DateTime dueDate, decimal totalAmount, string currency)
        {
            Id = Guid.NewGuid();
            InvoiceNumber = invoiceNumber;
            CustomerId = customerId;
            Date = date;
            DueDate = dueDate;
            TotalAmount = totalAmount;
            Currency = currency;
            Lines = new List<BillingLineEntity>();
            Customer = null!;
        }

        public string InvoiceNumber { get; private set; } = string.Empty;
        public Guid CustomerId { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Currency { get; private set; } = string.Empty;
        public List<BillingLineEntity> Lines { get; private set; }
        public CustomerEntity Customer { get; private set; }

        public void AddBillingLines(List<BillingLineEntity> billingLines)
        {
            Lines.AddRange(billingLines);
        }
    }
}
