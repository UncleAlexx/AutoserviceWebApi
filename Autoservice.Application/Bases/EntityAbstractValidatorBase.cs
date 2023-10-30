namespace Autoservice.Application.Bases;

internal abstract class EntityPropertiesValidator<TEntity> : AbstractValidator<TEntity> where TEntity : EntityBase
{
    public static readonly Predicate<string?> IsNullOrEmpty = v => v is null or "";
    
    protected EntityPropertiesValidator()
    {
        RuleFor(x => x).NotNull();
    }
}
