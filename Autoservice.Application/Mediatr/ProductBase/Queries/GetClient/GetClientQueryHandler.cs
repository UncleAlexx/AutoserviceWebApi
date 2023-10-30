namespace Autoservice.Application.ProductBase.Queries.GetClient;

public sealed class GetClientQueryHandler<TProduct> : IQueryHandler<GetClientQuery<TProduct>, ClientEntity, EntityResult<ClientEntity>>
    where TProduct : ProductEntity
{
    private readonly IProductBaseRepository<TProduct> _productRepository;
    private readonly IContragentBaseRepository<Client> _clientRepository;

    private readonly EntityExistsValidator<TProduct> _productExistsValidator;
    
    public GetClientQueryHandler(IProductBaseRepository<TProduct> productRepository, IContragentBaseRepository<Client> clientRepository,
        EntityExistsValidator<TProduct> productExistsValidator) =>
        (_productRepository, _clientRepository, _productExistsValidator) = (productRepository, clientRepository, productExistsValidator);

    public async Task<EntityResult<ClientEntity>> Handle(GetClientQuery<TProduct> request, CancellationToken token)
    {
        if ((await _productExistsValidator.ValidateAsync(request.ProductId, token)).IsValid)
        {
            var client = await _productRepository.GetClientAsync(request.ProductId, _clientRepository);
            return client is null ? Result<ClientEntity>.Failed<EntityResult<ClientEntity>>(new ForeignKeyNullException<TProduct>(ClientIdRaw, request.ProductId)) :
                Result<ClientEntity>.Success<EntityResult<ClientEntity>>(client);
        }
        return Result<ClientEntity>.Failed<EntityResult<ClientEntity>>(new EntityNotFoundException<TProduct>(request.ProductId, IdRaw));
    }
}
