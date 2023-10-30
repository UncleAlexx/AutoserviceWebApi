namespace Autoservice.Application.Common.Queries.GetAll;

public sealed class GetAllQueryHandler<TEntity> : ICollectionQueryHandler<GetAllQuery<TEntity>, TEntity, EntityCollectionResult<TEntity>> where TEntity: EntityBase
{
    private readonly IRepository<TEntity> _repository;

    public GetAllQueryHandler(IRepository<TEntity> repository) => _repository = repository;

    public Task<EntityCollectionResult<TEntity>> Handle(GetAllQuery<TEntity> request, CancellationToken token)
    {
        var all = _repository.GetAll();
        return Task.FromResult(all.Any() ? Result<ICollection<TEntity>>.Success<EntityCollectionResult<TEntity>>(all) :
            Result<ICollection<TEntity>>.Failed<EntityCollectionResult<TEntity>>(new EntitiesNotFoundException<TEntity, Missing>()));
    }
}
