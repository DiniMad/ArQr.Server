using FluentValidation;
using Resource.Api.Resources;

namespace Resource.Api.Validations
{
    public class RefreshTokenValidation : AbstractValidator<RefreshTokenResource>
    {
        public RefreshTokenValidation()
        {
            RuleFor(resource => resource.UserId).NotNull().NotEmpty();
            RuleFor(resource => resource.RefreshToken).NotNull().NotEmpty();
        }
    }
}