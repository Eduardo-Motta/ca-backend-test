using FluentValidation;

namespace Nexer.Finance.Domain.Commands
{
    public class PaginationValidator : AbstractValidator<PaginationCommand>
    {
        public PaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                .NotNull().WithMessage("Required field")
                .GreaterThan(0).WithMessage("The PageNumber must be greater than zero");

            RuleFor(x => x.PageSize)
                .NotNull().WithMessage("Required field")
                .GreaterThanOrEqualTo(5).WithMessage("The PageSize must be greater than or equal to 5");
        }
    }
}
