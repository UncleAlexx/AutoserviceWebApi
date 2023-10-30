using Autoservice.Infrastructure.DependencyInjection;

namespace Autoservice.Infrastructure.DependencyInjection;

public static class DbConnectionInjection
{
    private const string _connectionStringName = "AutoserviceConnectionString";
    public static void AddDbConnection(this IServiceCollection services)
    {
        services.AddDbContext<AutoserviceContext>(x =>
        {
            x.UseSqlServer(services.BuildServiceProvider().GetConnectionString());
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static string? GetConnectionString(this IServiceProvider provider) =>
        provider.GetRequiredService<IConfiguration>().GetConnectionString(_connectionStringName);
}
