using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class MakeUserAdminValidation : AbstractValidator<MakeUserAdminResource>
    {
        public MakeUserAdminValidation(IStringLocalizer<ResourcesPropertyNames> propertyNames)
        {
            RuleFor(resource => resource.UserId)
                .NotNull()
                .WithName(propertyNames[ResourcesPropertyNames.UserId]);
            
            RuleFor(resource => resource.Admin)
                .NotNull();
                // .WithName(propertyNames[ResourcesPropertyNames.Admin]);
        }
    }
}