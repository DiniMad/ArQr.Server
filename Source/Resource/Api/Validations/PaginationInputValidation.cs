using FluentValidation;
using Resource.Api.Resources;

namespace Resource.Api.Validations
{
    public class PaginationInputValidation : AbstractValidator<PaginationInputResource>
    {
        public PaginationInputValidation()
        {
            RuleFor(input => input.PageSize).InclusiveBetween(1,25);
            RuleFor(input => input.PageNumber).GreaterThan(0);
        }
    }
}