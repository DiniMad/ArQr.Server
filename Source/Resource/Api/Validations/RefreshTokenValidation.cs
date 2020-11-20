using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class RefreshTokenValidation : AbstractValidator<RefreshTokenResource>
    {
        public RefreshTokenValidation(IStringLocalizer<ResourcesPropertyNames> propertyNames)
        {
            RuleFor(resource => resource.UserId)
                .NotNull()
                .NotEmpty()
                .WithName(_ => propertyNames[ResourcesPropertyNames.UserId]);
            RuleFor(resource => resource.RefreshToken)
                .NotNull()
                .NotEmpty()
                .WithName(_ => propertyNames[ResourcesPropertyNames.RefreshToken]);
        }
    }
}