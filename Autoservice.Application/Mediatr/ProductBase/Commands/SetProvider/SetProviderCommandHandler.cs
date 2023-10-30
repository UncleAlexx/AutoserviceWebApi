namespace Autoservice.Application.ProductBase.Commands.SetProvider;

public sealed class SetProviderCommandHandler<TProduct> : ICommandHandler<SetProviderCommand<TProduct>, ProviderEntity, EntityResult<ProviderEntity>>
    where TProduct : ProductEntity
{
    private readonly IProductBaseRepository<TProduct> _productValidator;
    private readonly IContragentBaseRepository<ProviderEntity> _providerRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly EntityExistsValidator<TProduct> _productExistsValidator;
    private readonly EntityExistsValidator<ProviderEntity> _providerExistsValidator;
    private readonly SetProviderCommandValidator<TProduct> _setProviderValidator;

    public SetProviderCommandHandler(IProductBaseRepository<TProduct> tProductRepository, EntityExistsValidator<TProduct> productExistsVAlidator,
        IContragentBaseRepository<ProviderEntity> providerRepository, EntityExistsValidator<ProviderEntity> providerExistsValidator, IUnitOfWork unitOfWork,
        SetProviderCommandValidator<TProduct> setProviderValidator) =>
        (_productValidator, _providerRepository, _productExistsValidator, _providerExistsValidator, _setProviderValidator, _unitOfWork) = 
        (tProductRepository, providerRepository, productExistsVAlidator, providerExistsValidator, setProviderValidator, unitOfWork);

    public async Task<EntityResult<ProviderEntity>> Handle(SetProviderCommand<TProduct> request, CancellationToken token)
    {
        var productValidationResult = await _productExistsValidator.ValidateAsync(request.Id, token);
        var providerValidationResult = await _providerExistsValidator.ValidateAsync(request.ProviderId, token);
        var setProviderValidationResult = await  _setProviderValidator.ValidateAsync(request, token);
        if (new FluentValidation.Results.ValidationResult[] { productValidationResult, providerValidationResult, setProviderValidationResult }.IsValid())
        {
            try
            {
                await _unitOfWork.SaveChangesAsync();
                var result = await _productValidator.SetProviderAsync(request.Id, request.ProviderId, _providerRepository);
                return Result<ProviderEntity>.Success<EntityResult<ProviderEntity>>(result!);
            }
            catch (Exception ex)
            {
                return Result<ProviderEntity>.Failed<EntityResult<ProviderEntity>>(new DbUnhandledException(ex));
            }
        }
        return Result<ProviderEntity>.Failed<EntityResult<ProviderEntity>>(new ValidationException(productValidationResult.Errors.Concat
            (providerValidationResult.Errors).Concat(setProviderValidationResult.Errors)));
    }
}