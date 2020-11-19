using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Resource.Api.Validations;

namespace ArQr.Helper
{
    public static class MvcBuilderExtension
    {
        public static IMvcBuilder AddTheFluentValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(configuration =>
            {
                configuration.RegisterValidatorsFromAssemblyContaining<UserRegisterValidation>(lifetime: ServiceLifetime
                    .Singleton);
                configuration.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            return mvcBuilder;
        }
    }
}