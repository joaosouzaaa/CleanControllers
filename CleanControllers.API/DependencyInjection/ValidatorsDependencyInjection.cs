using CleanControllers.API.Entities;
using CleanControllers.API.Validators;
using FluentValidation;

namespace CleanControllers.API.DependencyInjection;

public static class ValidatorsDependencyInjection
{
    public static void AddValidatorsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Person>, PersonValidator>();
    }
}
