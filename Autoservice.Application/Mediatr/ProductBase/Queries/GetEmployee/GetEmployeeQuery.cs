namespace Autoservice.Application.ProductBase.Queries.GetEmployee;

public sealed record GetEmployeeQuery<TProduct>(Guid ProductId) : IQuery<EmployeeEntity, EntityResult<EmployeeEntity>> where TProduct : ProductEntity;
