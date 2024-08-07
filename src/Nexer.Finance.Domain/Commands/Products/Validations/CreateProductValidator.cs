using FluentValidation;

namespace Nexer.Finance.Domain.Commands.Products.Validations
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Required field");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Required field");
        }
    }
}
