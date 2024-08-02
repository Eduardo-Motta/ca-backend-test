using FluentValidation;

namespace Nexer.Finance.Domain.Commands.Customer.Validations
{
    public class FindAllCustomersValidator : AbstractValidator<FindAllCustomersCommand>
    {
        public FindAllCustomersValidator()
        {
            RuleFor(x => x.Pagination).SetValidator(new PaginationValidator());
        }
    }
}
