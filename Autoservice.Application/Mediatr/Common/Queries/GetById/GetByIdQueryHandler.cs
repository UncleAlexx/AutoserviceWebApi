namespace Autoservice.Application.Common.Queries.GetById;

public sealed class GetByIdQueryHandler<TEntity> : IQueryHandler<GetByIdQuery<TEntity>, TEntity, EntityResult<TEntity>> where TEntity : EntityBase 
{
    private readonly IRepository<TEntity> _repository;

    private readonly EntityExistsValidator<TEntity> _entityExistsValidator;

    public GetByIdQueryHandler(IRepository<TEntity> repository, EntityExistsValidator<TEntity> entityExistsValidator) =>
        (_repository, _entityExistsValidator) = (repository, entityExistsValidator);

    public async Task<EntityResult<TEntity>> Handle(GetByIdQuery<TEntity> request, CancellationToken token)
    {
        if ((await _entityExistsValidator.ValidateAsync(request.EntityId, token)).IsValid)
            return Result<TEntity>.Success<EntityResult<TEntity>>((await _repository.GetByIdAsync(request.EntityId))!);

        return Result<TEntity>.Failed<EntityResult<TEntity>>(new EntityNotFoundException<TEntity>(request.EntityId, IdRaw));
    }
}
