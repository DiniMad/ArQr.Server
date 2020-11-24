using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class PaginationInputValidation : AbstractValidator<PaginationInputResource>
    {
        public PaginationInputValidation(IStringLocalizer<ResourcesPropertyNames> propertyNames)
        {
            RuleFor(input => input.PageSize)
                .InclusiveBetween(1, 25)
                .WithName(_ => propertyNames[ResourcesPropertyNames.PageSize]);
            RuleFor(input => input.PageNumber)
                .GreaterThan(0)
                .WithName(_ => propertyNames[ResourcesPropertyNames.PageNumber]);
        }
    }
}