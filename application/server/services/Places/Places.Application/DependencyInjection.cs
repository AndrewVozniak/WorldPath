using System.Reflection;
using MediatR;
using Refit;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Places.Application.Common.Behaviors;
using Places.Application.Common.Mappings;
using Places.Application.Interfaces;

namespace Places.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services
            .AddRefitClient<IGooglePlaceApi>()
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/place/"));
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        return services;
    }
}