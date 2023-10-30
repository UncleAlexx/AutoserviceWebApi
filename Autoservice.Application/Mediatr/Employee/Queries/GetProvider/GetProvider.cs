namespace Autoservice.Application.Employee.Queries.GetProvider;

public sealed record GetProviderQuery(Guid EmployeeId) : IQuery<ProviderEntity, EntityResult<ProviderEntity>>;
