namespace Autoservice.Domain.Exceptions;

public sealed class ForeignKeyNullException<TEntity> : Exception where TEntity : EntityBase
{
    private readonly string _criteria;

    private readonly Guid _id;

    public ForeignKeyNullException(string criteria, Guid id) => (_criteria, _id) = (criteria, id);   

    public override string Message => $"foreign key {_criteria} for {typeof(TEntity).Name} with id = {_id} was null";
}