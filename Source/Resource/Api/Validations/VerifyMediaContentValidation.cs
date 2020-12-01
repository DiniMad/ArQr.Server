using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class VerifyMediaContentValidation: AbstractValidator<VerifyMediaContentResource>
    {
        public VerifyMediaContentValidation(IStringLocalizer<ResourcesPropertyNames> propertyNames)
        {
            RuleFor(resource => resource.MediaContentId)
                .NotNull()
                .WithName(_=>propertyNames[ResourcesPropertyNames.MediaContentId]);

            RuleFor(resource => resource.Verify)
                .NotNull()
                .WithName(_=>propertyNames[ResourcesPropertyNames.MediaContentId]);

        }
    }
}