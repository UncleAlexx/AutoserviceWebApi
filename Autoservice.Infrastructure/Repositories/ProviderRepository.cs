namespace Autoservice.Infrastructure.Repositories;

public sealed class ProviderRepository : Repository<Provider>, IProviderRepository
{
    private readonly IProductBaseRepository<Part> _partRepository;
    private readonly IProductBaseRepository<Car> _carRepository;
    private readonly IContragentBaseRepository<Client> _clientRepository;

    public ProviderRepository(IProductBaseRepository<Part> partRepository, IProductBaseRepository<Car> carRepository, 
        IContragentBaseRepository<Client> clientRepository, AutoserviceContext autoserviceContext) : base(autoserviceContext) =>
        (_partRepository, _carRepository, _clientRepository) = (partRepository, carRepository, clientRepository);

    public ICollection<Client> GetClients(Guid providerId)
    {
        var parts = _partRepository.GetAll();
        var cars = _carRepository.GetAll();
        var clients = _clientRepository.GetAll();
        var providerClientsIds = parts.Where(p => p.ProviderId == providerId).Select(p => p.ClientId).
            Concat(cars.Where(c => c.ProviderId == providerId).
            Select(c => c.ClientId)).Distinct();
        return clients.Where(c => providerClientsIds.Contains(c.Id)).ToList();
    }
}
 