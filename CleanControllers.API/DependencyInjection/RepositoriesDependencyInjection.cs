using CleanControllers.API.Data.Repositories;
using CleanControllers.API.Interfaces.Repositories;

namespace CleanControllers.API.DependencyInjection;

public static class RepositoriesDependencyInjection
{
    public static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();
    }
}
