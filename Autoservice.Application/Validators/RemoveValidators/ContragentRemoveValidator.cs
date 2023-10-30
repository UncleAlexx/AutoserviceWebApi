namespace Autoservice.Application.Validators.RemoveValidators;

internal sealed class ContragentBaseRemoveValidator<TContragent> : AbstractValidator<TContragent>, IRemoveValidator<TContragent> 
    where TContragent : ContragentEntity
{
    public ContragentBaseRemoveValidator(IProductBaseRepository<CarEntity> carRepository, IProductBaseRepository<PartEntity> partRepository) =>
        RuleFor(x => x.Id).ValidateRemoveReferences(carRepository, partRepository, Unsafe.As<Func<ProductEntity, Guid>, 
            Func<ProductEntity, Guid>>(ref Unsafe.AsRef(ValidationConstants.Selectors.ProductBaseSelector<TContragent>)), ClientIdRaw);
}
