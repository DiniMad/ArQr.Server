using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class UserLoginValidation : AbstractValidator<UserLoginResource>
    {
        public UserLoginValidation(IStringLocalizer<ResourcesPropertyNames>             propertyNames,
                                   IStringLocalizer<ResourcePropertyValidationMessages> validationMessages)
        {
            RuleFor(resource => resource.PhoneNumber)
                .NotNull()
                .Length(10)
                .WithName(_ => propertyNames[ResourcesPropertyNames.PhoneNumber]);
            RuleFor(resource => resource.Password)
                .NotNull()
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$")
                .WithMessage(_ => validationMessages[ResourcePropertyValidationMessages.PasswordRegex])
                .WithName(_ => propertyNames[ResourcesPropertyNames.Password]);
        }
    }
}