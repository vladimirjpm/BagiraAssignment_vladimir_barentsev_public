using Backend.Application.Abstractions;
using Backend.Infrastructure.Persistence;
using Backend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registers storage layer based on Storage:Provider config value.
    /// "inmemory" (default) → ConcurrentDictionary, no persistence between restarts.
    /// "postgres" → EF Core + Npgsql, requires ConnectionStrings:Default to be set.
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        var provider = configuration["Storage:Provider"] ?? "inmemory";

        if (string.Equals(provider, "postgres", StringComparison.OrdinalIgnoreCase))
        {
            var connectionString = configuration.GetConnectionString("Default")
                ?? throw new InvalidOperationException(
                    "Storage:Provider is 'postgres' but ConnectionStrings:Default is not set.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IScenarioRepository, EfScenarioRepository>();
            services.AddScoped<IEntityRepository, EfEntityRepository>();
        }
        else
        {
            // Singleton: in-memory state must outlive individual requests.
            services.AddSingleton<InMemoryStore>();
            services.AddScoped<IScenarioRepository, InMemoryScenarioRepository>();
            services.AddScoped<IEntityRepository, InMemoryEntityRepository>();
        }

        return services;
    }
}
