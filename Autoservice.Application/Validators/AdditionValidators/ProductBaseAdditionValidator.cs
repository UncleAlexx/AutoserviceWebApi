namespace Autoservice.Application.Validators.AdditionValidators;

internal sealed class ProductBaseAdditionValidator : AbstractValidator<ProductEntity>, IAdditionValidator<ProductEntity>
{
    public ProductBaseAdditionValidator(IRepository<ProviderEntity> providerRepository, 
        IRepository<ClientEntity> clientRepository)
    {
        RuleFor(x => x.ProviderId).KeyMustExist(providerRepository, ProviderIdRaw);
        RuleFor(x => x.ClientId).KeyMustExist(clientRepository, ClientIdRaw);
        RuleFor(x => x).MustHaveSameEmployeeId(providerRepository, clientRepository);
    }
}