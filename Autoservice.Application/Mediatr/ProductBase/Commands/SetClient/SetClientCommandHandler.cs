namespace Autoservice.Application.ProductBase.Commands.SetClient;

public sealed class SetClientCommandHandler<TProduct> : ICommandHandler<SetClientCommand<TProduct>, ClientEntity, EntityResult<ClientEntity>>
    where TProduct : ProductEntity
{
    private readonly IProductBaseRepository<TProduct> _productRepository;
    private readonly IContragentBaseRepository<Client> _clientRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly SetClientCommandValidator<TProduct> _setClientValidator;
    private readonly EntityExistsValidator<Client> _clientExistsValidator;
    private readonly EntityExistsValidator<TProduct> _productExistsValidator;

    public SetClientCommandHandler(IProductBaseRepository<TProduct> productRepository,  EntityExistsValidator<TProduct> productExistsValidator, 
        IUnitOfWork unitOfWork, EntityExistsValidator<Client> clientExistsValidator, SetClientCommandValidator<TProduct> setClientValidator, 
        IContragentBaseRepository<Client> clientRepository) =>
        (_productRepository, _clientRepository, _setClientValidator, _clientExistsValidator, _productExistsValidator, _unitOfWork) = 
        (productRepository, clientRepository, setClientValidator, clientExistsValidator, productExistsValidator, unitOfWork);

    public async Task<EntityResult<ClientEntity>> Handle(SetClientCommand<TProduct> request, CancellationToken token)
    {
        var clientExistsValidationResult = await _clientExistsValidator.ValidateAsync(request.ClientId ?? Guid.Empty, token);
        var setClientValidationResult = await _setClientValidator.ValidateAsync(request, token);
        var productExistsValidationResult = await _productExistsValidator.ValidateAsync(request.Id, token);
        if (new FluentValidation.Results.ValidationResult[] { clientExistsValidationResult, setClientValidationResult, productExistsValidationResult }.IsValid())
        {
            Client? result;
            try
            {
                result = await _productRepository.SetClientAsync(request.Id, request.ClientId, _clientRepository);
                await _unitOfWork.SaveChangesAsync();
                return Result<ClientEntity>.Success<EntityResult<ClientEntity>>(result!);
            }
            catch (Exception ex)
            {
                return Result<ClientEntity>.Failed<EntityResult<ClientEntity>>(new DbUnhandledException(ex));
            }
        }
        return Result<ClientEntity>.Failed<EntityResult<ClientEntity>>(new ValidationException(clientExistsValidationResult.Errors.
            Concat(setClientValidationResult.Errors).Concat(productExistsValidationResult.Errors)));
    }
}
