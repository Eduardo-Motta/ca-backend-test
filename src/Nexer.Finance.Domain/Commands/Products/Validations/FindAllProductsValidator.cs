using FluentValidation;

namespace Nexer.Finance.Domain.Commands.Products.Validations
{
    public class FindAllProductsValidator : AbstractValidator<FindAllProductsCommand>
    {
        public FindAllProductsValidator()
        {
            RuleFor(x => x.Pagination).SetValidator(new PaginationValidator());
        }
    }
}
