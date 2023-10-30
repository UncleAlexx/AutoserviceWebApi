namespace Autoservice.Application.Validators.RemoveValidators;

internal sealed class ProductBaseRemoveValidator<TProduct> : AbstractValidator<TProduct>, IRemoveValidator<TProduct> where TProduct : ProductEntity
{
    public ProductBaseRemoveValidator() { }
}
