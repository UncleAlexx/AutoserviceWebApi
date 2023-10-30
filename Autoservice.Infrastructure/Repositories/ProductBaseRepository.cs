namespace Autoservice.Infrastructure.Repositories;

public sealed class ProductBaseRepository<TProduct> : Repository<TProduct>, IProductBaseRepository<TProduct> where TProduct : ProductBase
{
    public ProductBaseRepository(AutoserviceContext autoserviceContext) : base(autoserviceContext) { }

    public async ValueTask<Provider?> SetProviderAsync(Guid id, Guid providerId, IContragentBaseRepository<Provider> providerRepository) =>
        await id.SetForeignKeyPropertyAsync(providerId, this, providerRepository, nameof(ProductBase.ProviderId), providerId);

    public async ValueTask<Client?> SetClientAsync(Guid id, Guid? clientId, IContragentBaseRepository<Client> clientRepository) =>
        await id.SetForeignKeyPropertyAsync(clientId, this, clientRepository, nameof(ProductBase.ClientId), clientId);

    public async ValueTask<Client?> GetClientAsync(Guid entityId, IContragentBaseRepository<Client> clientRepository) =>
        (await GetByIdAsync(entityId))?.ClientId?.GetEntity(clientRepository.GetAll(), x => x.Id);

    public async ValueTask<Provider?> GetProviderAsync(Guid entityId, IContragentBaseRepository<Provider> providerRepository) => 
        (await GetByIdAsync(entityId))?.ProviderId.GetEntity(providerRepository.GetAll(), x => x.Id);

    public async ValueTask<Employee?> GetEmployeeAsync(Guid entityId, IContragentBaseRepository<Provider> providerRepository,
        IRepository<Employee> employeeRepository)
    {
        TProduct? product = await GetByIdAsync(entityId);
        if (product is null)
            return null;
        Provider? provider = await providerRepository.GetByIdAsync(product.ProviderId);
        return await employeeRepository.GetByIdAsync(provider!.EmployeeId);
    }
}
