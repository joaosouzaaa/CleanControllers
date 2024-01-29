using CleanControllers.API.Interfaces.Mappers;
using CleanControllers.API.Mappers;

namespace CleanControllers.API.DependencyInjection;

public static class MappersDependencyInjection
{
    public static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPersonMapper, PersonMapper>();
    }
}
