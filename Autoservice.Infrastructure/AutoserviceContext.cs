namespace Autoservice.Infrastructure;

public sealed class AutoserviceContext : DbContext
{
    public AutoserviceContext(DbContextOptions<AutoserviceContext> db) : base(db) { }

    public DbSet<Part> Parts { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Provider> Providers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var clientModel = modelBuilder.Entity<Client>();
        var providerModel = modelBuilder.Entity<Provider>();
        var partModel = modelBuilder.Entity<Part>();
        var carModel = modelBuilder.Entity<Car>();
        clientModel.Build<Client, Employee>();
        providerModel.Build<Provider, Employee>(many: false);
        partModel.Build<Part, Provider>();
        partModel.Build<Part, Client>(required: false); 
        carModel.Build<Car, Provider>();
        carModel.Build<Car, Client>(required: false);
    }
}
