namespace Autoservice.Application.Common.Commands.Update;

public sealed class UpdateCommandHandler<TEntity> : ICommandHandler<UpdateCommand<TEntity>, TEntity, EntityResult<TEntity>> where TEntity: EntityBase
{
    private readonly IRepository<TEntity> _repository;

    private readonly IPropertiesValidator<TEntity> _propertiesValidator;
    private readonly IEntityProviderIdUniquenessValidator<TEntity> _providerIdUniquenessValidator;
    private readonly IUpdateValidator<TEntity> _updateValidator;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateCommandHandler(IRepository<TEntity> repository, IPropertiesValidator<TEntity> propertiesValidator,
         IEntityProviderIdUniquenessValidator<TEntity> providerIdUniquessValidator, IUnitOfWork unitOfWork, IUpdateValidator<TEntity> updateValidator) =>
        (_repository, _propertiesValidator, _providerIdUniquenessValidator, _updateValidator, _unitOfWork) = 
        (repository, propertiesValidator, providerIdUniquessValidator, updateValidator, unitOfWork);

    public async Task<EntityResult<TEntity>> Handle(UpdateCommand<TEntity> request, CancellationToken token)
    {
        FluentValidation.Results.ValidationResult[] validationResults = 
        {
            await _propertiesValidator.ValidateAsync(request.Entity, token),
            await _providerIdUniquenessValidator.ValidateAsync(request.Entity, token),
            await _updateValidator.ValidateAsync(request.Entity, token)
        };

        if (validationResults.IsValid())
        {
            TEntity? entity = _repository.Update(request.Entity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<TEntity>.Failed<EntityResult<TEntity>>(new DbUnhandledException(ex));
            }
            return Result<TEntity>.Success<EntityResult<TEntity>>(entity!);
        }
        return Result<TEntity>.Failed<EntityResult<TEntity>>(new FluentValidation.ValidationException(validationResults.SelectMany(x => x.Errors)));
    }
}
