namespace Autoservice.Application.ContragentBase.Queries.GetEmployee;

public sealed record GetEmployeeQuery<TContragent>(Guid EntityId) : IQuery<EmployeeEntity, EntityResult<EmployeeEntity>> where TContragent : ContragentEntity;
