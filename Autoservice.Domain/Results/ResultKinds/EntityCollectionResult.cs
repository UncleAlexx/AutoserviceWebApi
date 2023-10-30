namespace Autoservice.Domain.Results.ResultKinds;

public abstract class EntityCollectionResult <TEntity> : ResultBase<ICollection<TEntity>> where TEntity : EntityBase
{
}
