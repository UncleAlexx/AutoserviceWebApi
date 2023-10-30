namespace Autoservice.Domain.Interfaces;

public interface IProductBaseRepository<TProduct> : IRepository<TProduct> where TProduct : ProductBase
{
    ValueTask<Client?> SetClientAsync(Guid entityId, Guid? clientId, IContragentBaseRepository<Client> clientRepository);

    ValueTask<Provider?> SetProviderAsync(Guid entityId, Guid providerId, IContragentBaseRepository<Provider> providerRepository);

    ValueTask<Client?> GetClientAsync(Guid entityId, IContragentBaseRepository<Client> clientRepository);

    ValueTask<Provider?> GetProviderAsync(Guid entityId, IContragentBaseRepository<Provider> providerRepositor);

    ValueTask<Employee?> GetEmployeeAsync(Guid entityId, IContragentBaseRepository<Provider> providerRepositor, 
        IRepository<Employee> employeeRepository);
}
