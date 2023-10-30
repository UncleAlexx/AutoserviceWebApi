namespace Autoservice.Domain.Exceptions;

public sealed class PrimaryKeyConflictException<TEntity, TKeyValue> : Exception where TEntity : EntityBase
{
    private readonly TKeyValue _value;

    public PrimaryKeyConflictException(TKeyValue value) => _value = value;

    public override string Message => $"{typeof(TEntity).Name} with primary key  {_value} already exists in database";
}
