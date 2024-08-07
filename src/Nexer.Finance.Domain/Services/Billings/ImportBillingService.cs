using Microsoft.Extensions.Logging;
using Nexer.Finance.Domain.Dtos;
using Nexer.Finance.Domain.Entities;
using Nexer.Finance.Domain.ExternalApis;
using Nexer.Finance.Domain.Repositories;
using Nexer.Finance.Shared.Utils;

namespace Nexer.Finance.Domain.Services.Billings
{
    public class ImportBillingService : IImportBillingService
    {
        private readonly ILogger _logger;
        private readonly IBillingClientApi _billingClientApi;
        private readonly IBillingRepository _billingRepository;
        private readonly ICustomerRepository _customerRespository;
        private readonly IProductRepository _productRepository;

        public ImportBillingService(
            ILogger<ImportBillingService> logger,
            IBillingClientApi billingClientApi,
            IBillingRepository billingRepository,
            ICustomerRepository customerRespository,
            IProductRepository productRepository)
        {
            _logger = logger;
            _billingClientApi = billingClientApi;
            _billingRepository = billingRepository;
            _customerRespository = customerRespository;
            _productRepository = productRepository;
        }

        public async Task<Either<Error, BillingImportStatusDto>> ImportaAll(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching data from the billing api");
                var billings = await _billingClientApi.GetBillingAsync(cancellationToken);

                if (billings.Count() == 0)
                {
                    _logger.LogInformation("There is no billing to be imported");
                    return Either<Error, BillingImportStatusDto>.RightValue(new BillingImportStatusDto("There is no billing to be imported"));
                }

                _logger.LogInformation($"{billings.Count()} billing to be imported");

                BillingImportStatusDto billingImportStatus = await HandleBillingToImport(billings, cancellationToken);

                return Either<Error, BillingImportStatusDto>.RightValue(billingImportStatus);
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogError(ex, "A operação foi interrompida por timeout");
                return Either<Error, BillingImportStatusDto>.LeftValue(new Error("A operação foi interrompida por timeout. Try again in a few minutes"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating the customer");
                return Either<Error, BillingImportStatusDto>.LeftValue(new Error("An error occurred while creating the customer"));
            }
        }

        private async Task<BillingImportStatusDto> HandleBillingToImport(List<BillingDto> billings, CancellationToken cancellationToken)
        {
            BillingImportStatusDto billingImportStatus = new BillingImportStatusDto();

            foreach (var billing in billings)
            {
                var errors = new List<Error>();

                _logger.LogInformation("Checking if Invoice Number {InvoiceNumber} exists in the local database", billing.InvoiceNumber);

                try
                {
                    var billingFound = await _billingRepository.FindBillingByInvoiceNumberAsync(billing.InvoiceNumber, cancellationToken);

                    if (billingFound is not null)
                    {
                        _logger.LogInformation("Billing with InvoiceNumber {InvoiceNumber} already exists", billing.InvoiceNumber);
                        
                        var billingImportError = new BillingImportError(billing.InvoiceNumber, $"Billing with InvoiceNumber {billing.InvoiceNumber} already exists");
                        billingImportStatus.AddBillingImport(billingImportError);

                        continue;
                    }

                    var validatedDetails = await ValidateBillingDetails(billing, cancellationToken);
                    errors.AddRange(validatedDetails);

                    if (errors.Any())
                    {
                        _logger.LogInformation("Billing {InvoiceNumber} will not be imported. Erros: {errors}", billing.InvoiceNumber, errors);

                        billingImportStatus.AddBillingImport(MapBillingImportError(billing, errors));
                        continue;
                    }

                    var mappedBilling = MapBillingEntity(billing);

                    _logger.LogInformation("Importing billing {InvoiceNumber}", billing.InvoiceNumber);
                    await _billingRepository.CreateBillingAsync(mappedBilling, cancellationToken);

                    _logger.LogInformation("{InvoiceNumber} billing imported successfully", billing.InvoiceNumber);
                    billingImportStatus.AddBillingImport(new BillingImportSuccessful(billing.InvoiceNumber));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while processing billing with InvoiceNumber {InvoiceNumber}", billing.InvoiceNumber);
                    billingImportStatus.AddBillingImport(new BillingImportError(billing.InvoiceNumber, $"Error processing billing with InvoiceNumber {billing.InvoiceNumber}"));
                }
            }

            return billingImportStatus;
        }

        private BillingImportError MapBillingImportError(BillingDto billing, List<Error> errors)
        {
            var billingImportError = new BillingImportError(billing.InvoiceNumber);

            billingImportError.Errors.AddRange(errors.Select(x => x.Message).ToList());

            return billingImportError;
        }

        private async Task<List<Error>> ValidateBillingDetails(BillingDto billing, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            var customerError = await ValidateCustomer(billing.Customer, cancellationToken);
            var errorBillingLine = await ValidateBillingLine(billing.Lines, cancellationToken);

            errors.AddRange(customerError);
            errors.AddRange(errorBillingLine);

            return errors;
        }

        private async Task<List<Error>> ValidateCustomer(CustomerDto? customer, CancellationToken cancellationToken)
        {
            List<Error> errors = new List<Error>();

            if (customer is null)
            {
                _logger.LogInformation("There is no customer linked to billing");
                errors.Add(new Error($"There is no customer linked to billing"));

                return errors;
            }

            _logger.LogInformation("Checking if customer with Id {Id} exists in the local database", customer.Id);
            var customerFound = await _customerRespository.FindCustomerByIdAsync(customer.Id, cancellationToken);

            if (customerFound is null)
            {
                _logger.LogInformation("Customer with Id {Id} does not exist in the local database", customer.Id);
                errors.Add(new Error($"Customer with Id {customer.Id} does not exist in the local database"));
            }

            return errors;
        }

        private async Task<List<Error>> ValidateBillingLine(IEnumerable<BillingLineDto> lines, CancellationToken cancellationToken)
        {
            List<Error> errors = new List<Error>();

            if (lines is null || lines.Count() == 0)
            {
                _logger.LogInformation("There are no products linked to billing");
                errors.Add(new Error($"There are no products linked to billing"));

                return errors;
            }

            foreach (var line in lines)
            {
                _logger.LogInformation("Checking if product with Id {ProductId} exists in the local database", line.ProductId);
                var productFound = await _productRepository.FindProductByIdAsync(line.ProductId, cancellationToken);

                if (productFound is null)
                {
                    _logger.LogInformation("Product with Id {ProductId} does not exist in the local database", line.ProductId);
                    errors.Add(new Error($"Product with Id {line.ProductId} does not exist in the local database"));
                    continue;
                }
            }

            return errors;
        }

        private BillingEntity MapBillingEntity(BillingDto dto)
        {
            var billing = new BillingEntity(dto.InvoiceNumber, dto.Customer.Id, dto.Date, dto.DueDate, dto.TotalAmount, dto.Currency);
            var billingLines = dto.Lines.Select(x => MapBillingLineEntity(billing.Id, x)).ToList();
            billing.AddBillingLines(billingLines);

            return billing;
        }

        private BillingLineEntity MapBillingLineEntity(Guid billingId, BillingLineDto dto)
        {
            return new BillingLineEntity(billingId, dto.ProductId, dto.Quantity, dto.UnitPrice, dto.Subtotal);
        }

    }
}