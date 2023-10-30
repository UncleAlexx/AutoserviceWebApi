namespace Autoservice.Domain.Exceptions;

public sealed class EntitiesNotFoundException<TEntity, TKeyValue> : Exception where TEntity : EntityBase
{
    private readonly TKeyValue? _value;
    private readonly string?  _keyName;
    private readonly bool _isPrimary;

    public EntitiesNotFoundException(TKeyValue? value = default, string? keyName = null, bool isPrimary = false)
    {
        _keyName = keyName;
        _value = value;
        _isPrimary = isPrimary;
    }

    public override string Message => $"no {typeof(TEntity).Name} found{(_keyName is null or "" ? "" :
        $" with {(_isPrimary? ExceptionsConstants.PrimaryKeyRaw : ExceptionsConstants.ForeignKeyRaw)} named '{_keyName}' = {_value}")}";
}
