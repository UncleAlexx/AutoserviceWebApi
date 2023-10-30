namespace Autoservice.Application.Employee.Queries.GetClients;

public sealed record GetClientsQuery(Guid EmployeeId) : ICollectionQuery<ClientEntity, EntityCollectionResult<ClientEntity>>;
