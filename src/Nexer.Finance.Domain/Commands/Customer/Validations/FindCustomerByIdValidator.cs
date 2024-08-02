using FluentValidation;

namespace Nexer.Finance.Domain.Commands.Customer.Validations
{
    public class FindCustomerByIdValidator : AbstractValidator<FindCustomerByIdCommand>
    {
        public FindCustomerByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Required field")
                .GreaterThan(0).WithMessage("The Id must be greater than zero");
        }
    }
}
