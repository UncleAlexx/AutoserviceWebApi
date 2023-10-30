namespace Autoservice.Application.Common.Queries.GetAll;

public sealed record GetAllQuery<TEntity> : ICollectionQuery<TEntity, EntityCollectionResult<TEntity>> where TEntity : EntityBase;
