namespace Autoservice.Application.Validators;

internal sealed class DoesntExistValidator<TEntity> : AbstractValidator<TEntity>, IDoesntExistValidator<TEntity> where TEntity : EntityBase
{
    public DoesntExistValidator() => RuleFor(x => x.Id).Empty();
}
