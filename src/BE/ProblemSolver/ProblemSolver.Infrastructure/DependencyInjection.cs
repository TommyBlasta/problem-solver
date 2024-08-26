using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProblemSolver.Application.Infrastructure;
using ProblemSolver.Infrastructure.Persistence;
using ProblemSolver.Infrastructure.Persistence.Options;
using System.Reflection;

namespace ProblemSolver.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddDatabase(services, configuration);

        return services;
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var postgresDatabaseOptions = configuration.GetSection(PostgresDatabaseOptions.ConfigurationKey).Get<PostgresDatabaseOptions>();

        if (postgresDatabaseOptions == null)
        {
            throw new Exception("Db options not found.");
        }

        services.Configure<PostgresDatabaseOptions>(configuration.GetSection(PostgresDatabaseOptions.ConfigurationKey));

        services.AddDbContext<ApplicationDbContext>(delegate (IServiceProvider sp, DbContextOptionsBuilder builder)
        {
            var dbOptions = sp.GetRequiredService<IOptions<PostgresDatabaseOptions>>().Value;

            var dbContextOptionsBuilder = builder.UseNpgsql(dbOptions.ConnectionString, o =>
            o.MigrationsAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext))!.FullName)
            .MigrationsHistoryTable("ef_migrations"));

            if (dbOptions.EnableSensitiveLogging)
            {
                dbContextOptionsBuilder.LogTo(x => { }).EnableDetailedErrors().EnableSensitiveDataLogging();
            }

        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitializer>();
    }
}
