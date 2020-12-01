using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class CreateSupportedMediaExtensionValidation : AbstractValidator<CreateSupportedMediaExtensionResource>
    {
        public CreateSupportedMediaExtensionValidation(
            IStringLocalizer<ResourcePropertyValidationMessages> validationMessages,
            IStringLocalizer<ResourcesPropertyNames>             propertyNames)
        {
            RuleFor(resource => resource.Extension)
                .NotNull()
                .NotEmpty()
                .MaximumLength(8)
                .Must(s => !s.Contains('.'))
                .WithMessage(_ => validationMessages[ResourcePropertyValidationMessages.ExtensionWithoutPeriod])
                .WithName(_ => propertyNames[ResourcesPropertyNames.Extension]);
        }
    }
}