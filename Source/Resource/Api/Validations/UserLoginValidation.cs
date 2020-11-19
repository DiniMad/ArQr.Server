using FluentValidation;
using Resource.Api.Resources;

namespace Resource.Api.Validations
{
    public class UserLoginValidation : AbstractValidator<UserLoginResource>
    {
        public UserLoginValidation()
        {
            RuleFor(resource => resource.PhoneNumber).NotNull().NotEmpty().Length(10);
            RuleFor(resource => resource.Password).NotNull().Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$");
        }
    }
}