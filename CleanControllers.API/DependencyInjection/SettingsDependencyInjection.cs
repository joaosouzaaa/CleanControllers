using CleanControllers.API.Interfaces.Settings;
using CleanControllers.API.Settings.NotificationSettings;

namespace CleanControllers.API.DependencyInjection;

public static class SettingsDependencyInjection
{
    public static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();
    }
}
