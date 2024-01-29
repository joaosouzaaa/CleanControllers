using CleanControllers.API.Data.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace CleanControllers.API.Settings.MigrationSettings;

public static class MigrationHandler
{
    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<CleanControllersDbContext>();

        try
        {
            appContext.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}
