using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class CreateUploadSessionValidation : AbstractValidator<CreateUploadSessionResource>
    {
        public CreateUploadSessionValidation(IStringLocalizer<ResourcesPropertyNames> propertyNames)
        {
            RuleFor(resource => resource.MediaContentId)
                .NotNull()
                .WithName(_ => propertyNames[ResourcesPropertyNames.MediaContentId]);

            RuleFor(resource => resource.Extension)
                .NotNull()
                .NotEmpty()
                .MaximumLength(8)
                .WithName(_ => propertyNames[ResourcesPropertyNames.Extension]);

            RuleFor(resource => resource.TotalSizeInMb)
                .NotNull()
                .WithName(_ => propertyNames[ResourcesPropertyNames.TotalSizeInMb]);
        }
    }
}