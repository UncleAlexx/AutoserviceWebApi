namespace Autoservice.Application.Car.Queries;

public sealed class EntityExistsValidator<TEntity> : AbstractValidator<Guid> where TEntity : EntityBase
{
    public EntityExistsValidator(IRepository<TEntity> repository) 
    {
        var entityName = typeof(TEntity).Name;
        RuleFor(x => x).MustAsync(async (x, y) => (await repository.GetByIdAsync(x)) is not null).WithName(typeof(TEntity).Name).
            WithName($"{entityName} {MustExistMessage}").WithMessage(x => ValidationMessagesFactory.CreateKeyMustExistMessage<TEntity>(IdRaw, x.ToString()));
    }
}