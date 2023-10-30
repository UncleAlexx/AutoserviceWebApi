namespace Autoservice.Application.Common.Queries.GetAllByIds;

public sealed class GetAllByIdsQueryHandler<TEntity> : ICollectionQueryHandler<GetAllByIdsQuery<TEntity>, TEntity, EntityCollectionResult<TEntity>> 
    where TEntity : EntityBase
{
    private readonly IRepository<TEntity> _repository;

    private readonly GetAllByIdsQueryValidator<TEntity> _getAllValidator;

    public GetAllByIdsQueryHandler(IRepository<TEntity> repository, GetAllByIdsQueryValidator<TEntity> GetAllValidator) =>
        (_repository, _getAllValidator) = (repository, GetAllValidator);

    public async Task<EntityCollectionResult<TEntity>> Handle(GetAllByIdsQuery<TEntity> request, CancellationToken token)
    {
        var res = await _getAllValidator.ValidateAsync(request, token);
        if (res.IsValid && res.Errors.Count != request.ids.Length)
        {
            var result = _repository.GetAllByIds(request.ids);
            return result.Any() ? Result<ICollection<TEntity>>.Success<EntityCollectionResult<TEntity>>(result) :
                Result<ICollection<TEntity>>.Failed<EntityCollectionResult<TEntity>>(new EntitiesNotFoundException<TEntity, Guid>());
        }
        return Result<ICollection<TEntity>>.Failed<EntityCollectionResult<TEntity>>(new EntitiesNotFoundException<TEntity, Guid>());
    }
}
