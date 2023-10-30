namespace Autoservice.Infrastructure.Extensions;

internal static class IRepositoryExtensions
{
    private static Action<TTypeName, TPropertyValue>? GetSetPropertyDelegate<TTypeName, TPropertyValue>(string propertyName, bool useBaseType = true)
    {
        ReadOnlySpan<char> backingPart = stackalloc char[] { '_', '_', 'B', 'a', 'c', 'k', 'i', 'n', 'g', 'F', 'i', 'e', 'l', 'd' };
        FieldInfo? fieldInfo = (useBaseType ? typeof(TTypeName).BaseType : typeof(TTypeName))!.
            GetField(new PropertyBackingFieldNameBuilder(backingPart, propertyName), PropertyBackingFieldDelegateConstants.BackingFieldFlags);
        if (fieldInfo is null)
            return null;
        PropertyBackingFieldDelegateConstants.Arguments[0] = typeof(TTypeName);
        PropertyBackingFieldDelegateConstants.Arguments[1] = typeof(TPropertyValue);
        DynamicMethod method = new(string.Empty, null, PropertyBackingFieldDelegateConstants.Arguments);
        var il = method.GetILGenerator();
        il.EmitStoreField(fieldInfo);
        return Unsafe.As<Delegate, Action<TTypeName, TPropertyValue>>(ref Unsafe.AsRef(method.CreateDelegate(typeof(Action<TTypeName, TPropertyValue>))));
    }

    public async static ValueTask<TForegnKeyValue?> SetForeignKeyPropertyAsync<TEntity, TForegnKeyValue, TForeignKeyValue>(this Guid entityId, Guid? providerId, 
        IRepository<TEntity> repository, IRepository<TForegnKeyValue> repo2, string propertyName, TForeignKeyValue value, bool useBase = true) where TEntity : EntityBase where TForegnKeyValue : EntityBase
    {
        if (providerId == Guid.Empty || repo2.GetAll().Any(x => x.Id == providerId) is false)
            return null;
        await entityId.SetPropertyBase(propertyName, repository, value, useBase);
        return await repo2.GetByIdAsync(providerId?? Guid.Empty);
    }

    public async static ValueTask<TEntity?> SetPropertyAsync<TEntity, TPropertyValue>(this Guid entityId, string propName, 
        IRepository<TEntity> entityRepository, TPropertyValue propertyValue, bool useBaseType = true)
        where TEntity : EntityBase => await entityId.SetPropertyBase(propName, entityRepository, propertyValue, useBaseType);
  
    public static TEntity? GetEntity<TEntity>(this Guid firstKey, ICollection<TEntity> entities, Func <TEntity, Guid> secondKeySelector) 
        where TEntity : EntityBase
    {
        if (firstKey == Guid.Empty)
            return null;
        return entities.FirstOrDefault(x => secondKeySelector(x) == firstKey);
    }

    public static async ValueTask<ICollection<TSecondEntity>> GetEntitiesAsync<TFirstEntity, TSecondEntity>(this Guid tFirstKey, IRepository<TFirstEntity> 
        tFirstRepository, ICollection<TSecondEntity> tSecondRepository, Func<TSecondEntity, Guid?> TSecondKeySelector) where TFirstEntity : EntityBase 
        where TSecondEntity : EntityBase
    {
        var entity = await tFirstRepository.GetByIdAsync(tFirstKey);
        if (entity is null)
            return new List<TSecondEntity>();
        return tSecondRepository.Where(x => TSecondKeySelector(x) == entity.Id).ToList();
    }

    private async static ValueTask<TEntity?> SetPropertyBase<TEntity, TPropertyValue>(this Guid entityId, string propertyName, IRepository<TEntity> repository, 
        TPropertyValue propertyValue, bool useBaseType = true)
      where TEntity : EntityBase
    {
        var entity = await repository.GetByIdAsync(entityId);
        var set = GetSetPropertyDelegate<TEntity, TPropertyValue>(propertyName, useBaseType);
        if (set is null || entity is null)
            return null;
        set.Invoke(entity, propertyValue);
        return entity;
    }
}

public static class PropertyBackingFieldDelegateConstants
{
    private const byte _argsLength = 2;

    public const BindingFlags BackingFieldFlags = BindingFlags.NonPublic | BindingFlags.Instance;
    public static readonly Type[] Arguments = new Type[_argsLength];
}

file readonly ref struct PropertyBackingFieldNameBuilder
{
    private const char _lesserThanSign = '<';
    private const char _greaterThanSign = '>';
    private const char _kRaw = 'k';

    public static implicit operator string(in PropertyBackingFieldNameBuilder builder) => builder.ToString();

    private readonly ReadOnlySpan<char> _backingFieldPart;
    private readonly string _propertyName;

    public PropertyBackingFieldNameBuilder(in ReadOnlySpan<char> backingFieldPart, in string propertyName)
    {
        _backingFieldPart = backingFieldPart;
        _propertyName = propertyName;
    }

    public override string ToString()
    {
        Span<char> fff = stackalloc char[_propertyName.Length + _backingFieldPart.Length + 3];
        fff[0] = _lesserThanSign;
        var max = int.Max(_propertyName.Length, _backingFieldPart.Length);
        fff[1 + _propertyName.Length] = _greaterThanSign;
        fff[2 + _propertyName.Length] = _kRaw;
        for (int i = 0; i <= max; i++)
        {
            if (i < _propertyName.Length)
                fff[1 + i] = _propertyName[i];
            if (i < _backingFieldPart.Length)
                fff[3 + i + _propertyName.Length] = _backingFieldPart[i];
        }
        return fff.ToString();
    }
}
file static class ILGeneratorEtensions
{
    public static void EmitStoreField(this ILGenerator ilGenerator, in FieldInfo fieldInfo)
    {
        ilGenerator.Emit(OpCodes.Ldarg_S, 0);
        ilGenerator.Emit(OpCodes.Ldarg_S, 1);
        ilGenerator.Emit(OpCodes.Stfld, fieldInfo);
        ilGenerator.Emit(OpCodes.Ret);
    }
}