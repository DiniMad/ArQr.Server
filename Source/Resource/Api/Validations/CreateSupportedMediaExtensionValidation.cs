using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class CreateSupportedMediaExtensionValidation : AbstractValidator<CreateSupportedMediaExtensionResource>
    {
        public CreateSupportedMediaExtensionValidation(IStringLocalizer<ResourcesPropertyNames> propertyNames)
        {
            RuleFor(resource => resource.Extension)
                .NotNull()
                .NotEmpty()
                .MaximumLength(8)
                .WithName(_ => propertyNames[ResourcesPropertyNames.Extension]);
        }
    }
}