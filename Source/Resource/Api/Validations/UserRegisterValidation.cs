using FluentValidation;
using Resource.Api.Resources;

namespace Resource.Api.Validations
{
    public class UserRegisterValidation : AbstractValidator<UserRegisterResource>
    {
        public UserRegisterValidation()
        {
            RuleFor(resource => resource.PhoneNumber).NotNull().NotEmpty().Length(11);
            RuleFor(resource => resource.Password).NotNull().Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$");
        }
    }
}