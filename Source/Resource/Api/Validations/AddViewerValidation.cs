using FluentValidation;
using Resource.Api.Resources;

namespace Resource.Api.Validations
{
    public class AddViewerValidation : AbstractValidator<AddViewerResource>
    {
        public AddViewerValidation()
        {
            RuleFor(resource => resource.ViewerId).NotNull().GreaterThan(0);
        }
    }
}