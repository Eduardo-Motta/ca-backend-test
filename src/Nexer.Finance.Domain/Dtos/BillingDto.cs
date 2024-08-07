using System.Text.Json.Serialization;

namespace Nexer.Finance.Domain.Dtos
{
    public class BillingDto
    {
        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public CustomerDto? Customer { get; set; }
        public List<BillingLineDto> Lines { get; set; } = new List<BillingLineDto>();
    }
}
