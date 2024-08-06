using FluentValidation;

namespace Nexer.Finance.Domain.Commands.Products.Validations
{
    public class FindProductByIdValidator : AbstractValidator<FindProductByIdCommand>
    {
        public FindProductByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Required field");
        }
    }
}
