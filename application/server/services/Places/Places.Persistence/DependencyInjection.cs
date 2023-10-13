using Microsoft.Extensions.DependencyInjection;
using Places.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Places.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMongoDb, MongoDb>();
        return services;
    }
}