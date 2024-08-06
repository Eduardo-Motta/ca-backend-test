using FluentValidation;

namespace Nexer.Finance.Domain.Commands.Products.Validations
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Required field");
        }
    }
}
