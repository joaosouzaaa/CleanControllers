using CleanControllers.API.Interfaces.Services;
using CleanControllers.API.Services;

namespace CleanControllers.API.DependencyInjection;

public static class ServicesDependencyInjection
{
    public static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();
    }
}
