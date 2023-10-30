namespace Autoservice.Domain.Exceptions;

public sealed class ForeignKeyNotFoundException<TBaseEntity, TForeignKeyEntity> : Exception where TForeignKeyEntity : EntityBase
{
    private readonly Guid _primaryKeyValue;
    private readonly string _primaryKeyName;

    public ForeignKeyNotFoundException(in string primaryKeyName, in Guid primaryKeyValue) => (_primaryKeyName, _primaryKeyValue) = 
        (primaryKeyName, primaryKeyValue);

    public override string Message => $"{typeof(TForeignKeyEntity).Name} with {typeof(TBaseEntity).Name} {ExceptionsConstants.PrimaryKeyRaw} named " +
        $"'{_primaryKeyName}' = '{_primaryKeyValue}' was not found";
}
