namespace Autoservice.Application.Common.Commands.Add;

public sealed class AddCommandHandler<TEntity, TBase> : ICommandHandler<AddCommand<TEntity>, TEntity, EntityResult<TEntity>> where TEntity : TBase
    where TBase : EntityBase
{
    private readonly IRepository<TEntity> _repository;

    private readonly IPropertiesValidator<TEntity> _propertiesValidator;
    private readonly IAdditionValidator<TBase> _additionValidator;  
    private readonly IDoesntExistValidator<TEntity> _doesntExistsValidator;
    private readonly IEntityProviderIdUniquenessValidator<TEntity> _providerIdUniquenessValidator;

    private readonly IUnitOfWork _unitOfWork;

    public AddCommandHandler(IRepository<TEntity> repository, IPropertiesValidator<TEntity> propertiesValidator, 
        IAdditionValidator<TBase> additionValidator, IDoesntExistValidator<TEntity> doesntExistsValidator, IUnitOfWork unitOfWork,
        IEntityProviderIdUniquenessValidator<TEntity> providerIdUniquenessValidator) =>
        (_repository, _propertiesValidator, _additionValidator, _doesntExistsValidator, _unitOfWork, _providerIdUniquenessValidator) =  
        (repository, propertiesValidator, additionValidator, doesntExistsValidator, unitOfWork, providerIdUniquenessValidator);

    public async Task<EntityResult<TEntity>> Handle(AddCommand<TEntity> request, CancellationToken token)
    {
        var validationResults = await Task.WhenAll(_providerIdUniquenessValidator.ValidateAsync(request.Entity, token),
            _doesntExistsValidator.ValidateAsync(request.Entity, token), _propertiesValidator.ValidateAsync(request.Entity, token), 
            _additionValidator.ValidateAsync(request.Entity, token));

        if (validationResults.IsValid())
        {
            TEntity addedTEntity = (await _repository.AddAsync(request.Entity))!;
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return Result<TEntity>.Failed<EntityResult<TEntity>>(new DbUnhandledException(ex));
            }
            return Result<TEntity>.Success<EntityResult<TEntity>>(addedTEntity);
        }
        return Result<TEntity>.Failed<EntityResult<TEntity>>(new ValidationException(validationResults.SelectMany(x => x.Errors)));
    }
}
