namespace Autoservice.Infrastructure.Repositories;

public sealed class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    private readonly IContragentBaseRepository<Provider> _providerRepository;
    private readonly IContragentBaseRepository<Client> _clientRepository;
    private readonly IProductBaseRepository<Part> _partRepository;
    private readonly IProductBaseRepository<Car> _carRepository;

    public EmployeeRepository(AutoserviceContext autoserviceContext, IContragentBaseRepository<Provider> providerRepository,
        IContragentBaseRepository<Client> clientRepository, IProductBaseRepository<Part> partRepository, IProductBaseRepository<Car> carRepository)
        : base(autoserviceContext) => (_clientRepository, _providerRepository, _carRepository, _partRepository) = 
            (clientRepository, providerRepository, carRepository, partRepository);

    public Provider? GetProvider(Guid employeeId) => employeeId.GetEntity(_providerRepository.GetAll(), x => x.EmployeeId);

    public async ValueTask<ICollection<Client>> GetClientsAsync(Guid employeeId) => 
        await employeeId.GetEntitiesAsync(this, _clientRepository.GetAll(), x => x.EmployeeId)!;
    
    public async ValueTask<double> GetRevenueAsync(Guid employeeId)
    {
        var clients = await GetClientsAsync(employeeId);
        var provider = GetProvider(employeeId);
        var cars = _carRepository.GetAll();
        var parts = _partRepository.GetAll();
        if (provider is null || cars.Any() is false && parts.Any() is false)
            return 0;
        return cars.Where(y => clients.Any(x => x.Id == y.ClientId)).Select(x => x.Cost).Sum() +
           parts.Where(y => clients.Any(x => x.Id == y.ClientId)).Select(x => x.Cost).Sum() -
             cars.Where(y => y.ProviderId == provider.Id).Select(x => x.Cost).Sum() -
              parts.Where(y => y.ProviderId == provider.Id).Select(x => x.Cost).Sum();
    }
}
