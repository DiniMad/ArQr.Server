using System.Linq;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parbad.Storage.EntityFrameworkCore.Context;

namespace ArQr.Helper
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder ApplyDatabasesMigrations(this IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<DatabaseMigrator>();
            var parbadDataContext = serviceScope.ServiceProvider.GetRequiredService<ParbadDataContext>();
            if (parbadDataContext.Database.GetPendingMigrations().Any()) parbadDataContext.Database.Migrate();
            return app;
        }
    }
}