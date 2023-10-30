using Autoservice.Application.ProductBase.Commands.SetProvider;

namespace Autoservice.Application.Validators.RemoveValidators;

public sealed class SetProviderCommandValidator<TProduct> : AbstractValidator<SetProviderCommand<TProduct>> where TProduct : ProductEntity
{
    public SetProviderCommandValidator(IRepository<TProduct> productRepository)
    {
        RuleFor(x => x.Id).MustAsync(async (x, y) =>
        {
            var product = await productRepository.GetByIdAsync(x);
            if (product is null)
                return false;
            return product.ClientId.HasValue is false;
        }).WithMessage(CreateProviderIdErrorMessage(typeof(TProduct).Name));
    }
}
