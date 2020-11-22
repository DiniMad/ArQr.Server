using FluentValidation;
using Resource.Api.Resources;

namespace Resource.Api.Validations
{
    public class PaginationInputValidation : AbstractValidator<PaginationInputResource>
    {
        public PaginationInputValidation()
        {
            RuleFor(input => input.PageSize).GreaterThan(0);
            RuleFor(input => input.PageNumber).GreaterThan(0);
        }
    }
}