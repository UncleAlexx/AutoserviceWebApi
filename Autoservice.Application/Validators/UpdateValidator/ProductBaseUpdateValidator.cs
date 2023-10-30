namespace Autoservice.Application.Validators.UpdateValidators;

internal sealed class ProductBaseUpdateValidator<TProduct> : AbstractValidator<TProduct>, IUpdateValidator<TProduct> where TProduct : ProductEntity
{
    public ProductBaseUpdateValidator(IContragentBaseRepository<ProviderEntity> providerRepository,
        IContragentBaseRepository<ClientEntity> clientRepository) =>
        RuleFor(x => x).MustHaveSameEmployeeId(providerRepository, clientRepository);
}
