using FluentValidation;

namespace Nexer.Finance.Domain.Commands.Customer.Validations
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Required field");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Required field")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Required field");
        }
    }
}
