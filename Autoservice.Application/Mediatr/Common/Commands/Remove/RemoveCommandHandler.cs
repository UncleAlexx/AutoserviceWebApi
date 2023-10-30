namespace Autoservice.Application.Common.Commands.Remove;

public sealed class RemoveCommandHandler<TEntity> : ICommandHandler<RemoveCommand<TEntity>, TEntity, EntityResult<TEntity>> where TEntity: EntityBase
{
    private readonly IRepository<TEntity> _repository;

    private readonly EntityExistsValidator<TEntity> _entityExistsValidator;
    private readonly IRemoveValidator<TEntity>  _entityRemoveValidator;

    private readonly IUnitOfWork _unitOfWork;

    public RemoveCommandHandler(IRepository<TEntity> repository, EntityExistsValidator<TEntity> entityEistsValidator,
        IUnitOfWork unitOfWork, IRemoveValidator<TEntity> entityRemoveValidator) =>
        (_repository, _entityExistsValidator, _entityRemoveValidator, _unitOfWork) = (repository, entityEistsValidator, entityRemoveValidator, unitOfWork);

    public async Task<EntityResult<TEntity>> Handle(RemoveCommand<TEntity> request, CancellationToken token)
    {
        TEntity? t = await _repository.GetByIdAsync(request.Id);
        if (t is null)
            return Result<TEntity>.Failed<EntityResult<TEntity>>(new EntityNotFoundException<TEntity>(request.Id, IdRaw));
        var validationResult = await Task.WhenAll(_entityRemoveValidator.ValidateAsync(t, token), _entityExistsValidator.ValidateAsync(request.Id, token));
        if (validationResult.IsValid())
        {
            var result = _repository.Remove(t);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<TEntity>.Failed<EntityResult<TEntity>>(new DbUnhandledException(ex));
            }
            return Result<TEntity>.Success<EntityResult<TEntity>>(result);
        }
        return Result<TEntity>.Failed<EntityResult<TEntity>>(new
            FluentValidation.ValidationException(validationResult.SelectMany(x => x.Errors)));
    }
}
