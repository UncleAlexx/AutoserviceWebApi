namespace Autoservice.Domain.Interfaces;

public interface IProviderRepository : IRepository<Provider>
{
    ICollection<Client> GetClients(Guid providerId);
}
