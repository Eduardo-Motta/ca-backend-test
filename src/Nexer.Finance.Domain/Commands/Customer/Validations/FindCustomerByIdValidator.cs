using FluentValidation;

namespace Nexer.Finance.Domain.Commands.Customer.Validations
{
    public class FindCustomerByIdValidator : AbstractValidator<FindCustomerByIdCommand>
    {
        public FindCustomerByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Required field");
        }
    }
}
