namespace Autoservice.Application.Validators.ProviderIdUniqunessValidators;

internal sealed class EntityProviderIdUniquenessValidator<TEntity> : AbstractValidator<TEntity>,
    IEntityProviderIdUniquenessValidator<TEntity> where TEntity : EntityBase
{
    public EntityProviderIdUniquenessValidator()
    {
    }
}
