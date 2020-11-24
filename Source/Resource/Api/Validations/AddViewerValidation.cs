using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class AddViewerValidation : AbstractValidator<AddViewerResource>
    {
        public AddViewerValidation(IStringLocalizer<ResourcesPropertyNames> propertyNames)
        {
            RuleFor(resource => resource.ViewerId)
                .NotNull()
                .GreaterThan(0)
                .WithName(_ => propertyNames[ResourcesPropertyNames.ViewerId]);
        }
    }
}