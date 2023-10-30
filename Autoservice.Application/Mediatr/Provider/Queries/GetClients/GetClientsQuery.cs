namespace Autoservice.Application.Provider.Queries.GetClients;

public sealed record GetClientsQuery(Guid ProviderId) : ICollectionQuery<ClientEntity, EntityCollectionResult<ClientEntity>>;
