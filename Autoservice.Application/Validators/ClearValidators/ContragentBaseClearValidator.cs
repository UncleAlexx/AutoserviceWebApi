namespace Autoservice.Application.Validators.ClearValidators;

internal sealed class ContragentBaseClearValidator <TContragent> : AbstractValidator<ICollection<TContragent>>, IClearValidator<TContragent> where TContragent 
    : ContragentEntity
{
    public ContragentBaseClearValidator(IProductBaseRepository<PartEntity> partRepository,
        IProductBaseRepository<CarEntity> productRepository) =>
        RuleForEach(x => x.Select(x => x.Id)).ValidateRemoveReferencesForEach(partRepository, Unsafe.As<Func<ProductEntity,Guid>, Func<ProductEntity, Guid>>(
            ref Unsafe.AsRef(ValidationConstants.Selectors.ProductBaseSelector<TContragent>)), productRepository, ClientIdRaw);
}
