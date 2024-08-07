using System.Text.Json.Serialization;

namespace Nexer.Finance.Domain.Dtos
{
    public class BillingLineDto
    {
        public Guid ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
