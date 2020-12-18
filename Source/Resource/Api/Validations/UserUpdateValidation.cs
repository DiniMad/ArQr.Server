using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class UserUpdateValidation : AbstractValidator<UserUpdateResource>
    {
        public UserUpdateValidation(IStringLocalizer<ResourcesPropertyNames>             propertyNames,
                                    IStringLocalizer<ResourcePropertyValidationMessages> validationMessages)
        {
            RuleFor(resource => resource.PhoneNumber)
                .Length(10)
                .WithName(_ => propertyNames[ResourcesPropertyNames.PhoneNumber]);
            RuleFor(resource => resource.Password)
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$")
                .WithMessage(_ => validationMessages[ResourcePropertyValidationMessages.PasswordRegex])
                .WithName(_ => propertyNames[ResourcesPropertyNames.Password]);
        }
    }
}