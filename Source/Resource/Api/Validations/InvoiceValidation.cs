using FluentValidation;
using Resource.Api.Resources;

namespace Resource.Api.Validations
{
    public class InvoiceValidation : AbstractValidator<InvoiceResource>
    {
        public InvoiceValidation()
        {
            RuleFor(resource => resource.Service).NotNull().GreaterThan((byte) 0);
            RuleFor(resource => resource.Quantity).NotNull().GreaterThan((byte) 0);
        }
    }
}