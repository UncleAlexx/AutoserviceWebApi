namespace Autoservice.Application.Common.Queries.GetById;

public sealed record GetByIdQuery<TEntity>(Guid EntityId): IQuery<TEntity, EntityResult<TEntity>> where TEntity : EntityBase;
