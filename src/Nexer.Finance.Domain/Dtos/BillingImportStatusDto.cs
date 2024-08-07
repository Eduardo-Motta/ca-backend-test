namespace Nexer.Finance.Domain.Dtos
{
    public class BillingImportStatusDto
    {
        public BillingImportStatusDto() { }

        public BillingImportStatusDto(string message)
        {
            Message = message;
        }

        public List<BillingImportSuccessful> BillingImportSuccessful { get; set; } = new List<BillingImportSuccessful>();
        public List<BillingImportError> BillingImportError { get; set; } = new List<BillingImportError>();
        public string Message { get; private set; } = string.Empty;

        public void AddBillingImport(IBillingImportStatus billingImportStatus)
        {
            if(billingImportStatus is null)
            {
                return;
            }

            if(billingImportStatus is BillingImportSuccessful)
            {
                BillingImportSuccessful.Add((BillingImportSuccessful)billingImportStatus);
                return;
            }

            BillingImportError.Add((BillingImportError)billingImportStatus);
        }
    }

    public class BillingImportSuccessful : IBillingImportStatus
    {
        public BillingImportSuccessful(string invoiceNumber)
        {
            InvoiceNumber = invoiceNumber;
        }

        public string InvoiceNumber { get; private set; }
    }

    public class BillingImportError : IBillingImportStatus
    {
        public BillingImportError(string invoiceNumber)
        {
            InvoiceNumber = invoiceNumber;
            Errors = new List<string>();
        }

        public BillingImportError(string invoiceNumber, string message)
        {
            InvoiceNumber = invoiceNumber;
            Errors = new List<string>
            {
                message
            };
        }

        public string InvoiceNumber { get; private set; }
        public List<string> Errors { get; private set; }
    }

    public interface IBillingImportStatus
    {
        string InvoiceNumber { get; }
    }
}
