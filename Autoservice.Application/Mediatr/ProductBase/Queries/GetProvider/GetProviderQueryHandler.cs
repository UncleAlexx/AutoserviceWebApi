namespace Autoservice.Application.ProductBase.Queries.GetProvider;

public sealed class GetProviderQueryHandler<TProduct> : IQueryHandler<GetProviderQuery<TProduct>, ProviderEntity, EntityResult<ProviderEntity>> 
    where TProduct : ProductEntity
{
    private readonly IProductBaseRepository<TProduct> _productRepository;
    private readonly IContragentBaseRepository<ProviderEntity> _providerRepository;

    private readonly EntityExistsValidator<TProduct> _productExistsValidator;

    public GetProviderQueryHandler(IProductBaseRepository<TProduct> productRepository, EntityExistsValidator<TProduct> productExistsalidator,
        IContragentBaseRepository<ProviderEntity> providerRepository) =>
        (_productExistsValidator, _productRepository, _providerRepository) = (productExistsalidator, productRepository, providerRepository);

    public async Task<EntityResult<ProviderEntity>> Handle(GetProviderQuery<TProduct> request, CancellationToken token)
    {
        if ((await _productExistsValidator.ValidateAsync(request.ProductId, token)).IsValid)
        {
            ProviderEntity provider = (await _productRepository.GetProviderAsync(request.ProductId, _providerRepository))!;
            return Result<ProviderEntity>.Success<EntityResult<ProviderEntity>>(provider);
        }
        return Result<ProviderEntity>.Failed<EntityResult<ProviderEntity>>(new EntityNotFoundException<TProduct>(request.ProductId, IdRaw));
    }
}
