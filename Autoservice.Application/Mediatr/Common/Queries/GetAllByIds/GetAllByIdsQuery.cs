namespace Autoservice.Application.Common.Queries.GetAllByIds;

public sealed record GetAllByIdsQuery<TEntity>(Guid[] ids) : ICollectionQuery<TEntity, EntityCollectionResult<TEntity>> where TEntity : EntityBase;
