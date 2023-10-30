using Autoservice.Application.ProductBase.Commands.SetClient;

namespace Autoservice.Application.Validators.RemoveValidators;

public sealed class SetClientCommandValidator<TProduct> : AbstractValidator<SetClientCommand<TProduct>> where TProduct : ProductEntity
{
    public SetClientCommandValidator(IContragentBaseRepository<ClientEntity> contragentRepository, IProductBaseRepository<TProduct> productRepository,
        IContragentBaseRepository<ProviderEntity> providerRepository)
    {
        RuleFor(x => x).MustAsync(async (x, y) =>
        {
            if (x.ClientId is null)
                return true;
            var product = await productRepository.GetByIdAsync(x.Id);
            if (product is null)
                return false;
            var client = await contragentRepository.GetByIdAsync(x.ClientId.Value);
            var provider = await providerRepository.GetByIdAsync(product.ProviderId);
            return client is not null && client.EmployeeId == provider!.EmployeeId;
        }).WithName(ClientIdRaw).WithMessage(CreateClientIdErrorMessage(typeof(TProduct).Name));
    }
}
