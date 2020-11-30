using FluentValidation;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace Resource.Api.Validations
{
    public class CreateServiceValidation : AbstractValidator<CreateServiceResource>
    {
        public CreateServiceValidation(IStringLocalizer<ResourcesPropertyNames> propertyNames)
        {
            RuleFor(resource => resource.Title)
                .NotNull()
                .NotEmpty()
                .WithName(_ => propertyNames[ResourcesPropertyNames.Title]);

            RuleFor(resource => resource.Description)
                .NotNull()
                .NotEmpty()
                .WithName(_ => propertyNames[ResourcesPropertyNames.Description]);

            RuleFor(resource => resource.UnitPriceInThousandToman)
                .NotNull()
                .GreaterThan(0)
                .WithName(_ => propertyNames[ResourcesPropertyNames.UnitPriceInThousandToman]);

            RuleFor(resource => resource.ProductType)
                .NotNull()
                .IsEnumName(typeof(ProductType))
                .WithName(_ => propertyNames[ResourcesPropertyNames.ProductType]);

            RuleFor(resource => resource.Constraint)
                .NotNull()
                .GreaterThan(0)
                .WithName(_ => propertyNames[ResourcesPropertyNames.Constraint]);
        }
    }
}