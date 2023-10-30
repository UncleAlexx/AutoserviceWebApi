namespace Autoservice.Infrastructure.DependencyInjection;

public static class RepositoriesInjection
{
    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IContragentBaseRepository<>), typeof(ContragentBaseRepository<>));
        builder.Services.AddScoped(typeof(IProductBaseRepository<>), typeof(ProductBaseRepository<>));
        builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<IProviderRepository, ProviderRepository>(); 
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IRepository<Part>, ProductBaseRepository<Part>>();
        builder.Services.AddScoped<IRepository<Car>, ProductBaseRepository<Car>>();
        builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();
        builder.Services.AddScoped<IRepository<Client>, ContragentBaseRepository<Client>>();
        builder.Services.AddScoped<IRepository<Car>, ProductBaseRepository<Car>>();
        return builder;
    }
}
