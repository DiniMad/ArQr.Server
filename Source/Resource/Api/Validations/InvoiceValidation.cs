using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class InvoiceValidation : AbstractValidator<InvoiceResource>
    {
        public InvoiceValidation(IStringLocalizer<ResourcesPropertyNames> propertyNames)
        {
            RuleFor(resource => resource.Service)
                .NotNull()
                .GreaterThan((byte) 0)
                .WithName(_ => propertyNames[ResourcesPropertyNames.Service]);
            RuleFor(resource => resource.Quantity)
                .NotNull()
                .GreaterThan((byte) 0)
                .WithName(_ => propertyNames[ResourcesPropertyNames.Quantity]);
        }
    }
}