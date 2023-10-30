namespace Autoservice.Application.Validators.ClearValidators;

internal sealed class ProductBaseClearValidator<TProduct> : AbstractValidator<ICollection<TProduct>>, IClearValidator<TProduct> where TProduct : ProductEntity
{
    public ProductBaseClearValidator()
    {
    }
}
