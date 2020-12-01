using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class UploadCompletedValidation : AbstractValidator<UploadCompletedResource>
    {
        public UploadCompletedValidation(IStringLocalizer<ResourcesPropertyNames> propertyNames)
        {
            RuleFor(resource => resource.MediaContentId)
                .NotNull()
                .GreaterThan(0)
                .WithName(_ => propertyNames[ResourcesPropertyNames.MediaContentId]);
        }
    }
}