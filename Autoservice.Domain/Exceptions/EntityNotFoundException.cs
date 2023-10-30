namespace Autoservice.Domain.Exceptions;

public sealed class EntityNotFoundException <T> : Exception where T : EntityBase
{
    private readonly Guid _value;
    private readonly string _criteria;

    public EntityNotFoundException(Guid value, string criteria) => (_criteria, _value) = (criteria, value);

    public override string Message => $"{typeof(T).Name} with {ExceptionsConstants.PrimaryKeyRaw} named '{_criteria}' = '{_value}' was not found";
}
