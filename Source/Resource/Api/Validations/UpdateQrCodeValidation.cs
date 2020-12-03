using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class UpdateQrCodeValidation : AbstractValidator<UpdateQrCodeResource>
    {
        public UpdateQrCodeValidation(IStringLocalizer<ResourcesPropertyNames> propertyNames)
        {
            RuleFor(resource => resource.Title)
                .MaximumLength(32)
                .WithName(_ => propertyNames[ResourcesPropertyNames.Title]);

            RuleFor(resource => resource.Description)
                .MaximumLength(128)
                .WithName(_ => propertyNames[ResourcesPropertyNames.Description]);

            RuleFor(resource => resource.AssociatedWebsite)
                .MaximumLength(64)
                .WithName(_ => propertyNames[ResourcesPropertyNames.AssociatedWebsite]);

            RuleFor(resource => resource.AssociatedPhoneNumber)
                .Length(10)
                .WithName(_ => propertyNames[ResourcesPropertyNames.AssociatedPhoneNumber]);

            RuleFor(resource => resource.MediaContentId)
                .GreaterThanOrEqualTo(0)
                .WithName(_ => propertyNames[ResourcesPropertyNames.MediaContentId]);
        }
    }
}