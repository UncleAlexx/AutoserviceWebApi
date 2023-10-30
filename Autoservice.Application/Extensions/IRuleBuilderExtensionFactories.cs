namespace Autoservice.Application.Extensions;

internal static partial class IRuleBuilderExtensions
{
    public static IRuleBuilder<TContragent, TContragent> MustHaveEmployeeIdIntegrity<TContragent>(this IRuleBuilder<TContragent, TContragent> validator, 
        IRepository<CarEntity> carRepository, IRepository<ProviderEntity> providerRepository, IRepository<PartEntity> partRepository, 
        IRepository<ClientEntity> clientRepository, IRepository<TContragent> contragentRepository, string contragentName, AutoserviceContext context) 
        where TContragent : ContragentEntity
        => validator.MustHaveEmployeeIdIntegrityFactory(carRepository, providerRepository, partRepository, clientRepository, contragentName, 
            contragentRepository, context);

    public static IRuleBuilder<SetEmployeeCommand<TContragent>, SetEmployeeCommand<TContragent>> MustHaveEmployeeIdIntegrity<TContragent>(
    this IRuleBuilder<SetEmployeeCommand<TContragent>, SetEmployeeCommand<TContragent>> validator, IRepository<CarEntity> carRepository, 
       IRepository<ProviderEntity> providerRepository, IRepository<PartEntity> partRepository, IRepository<ClientEntity> clientRepository,
       string contragentName, IRepository<TContragent> contragentRepository) where TContragent : ContragentEntity
       => validator.MustHaveEmployeeIdIntegrityFactory(carRepository, providerRepository, partRepository, clientRepository, contragentName, contragentRepository);

    public static IRuleBuilder<TProduct, string?> MustBeNullOrBetween<TProduct>(this IRuleBuilder<TProduct, string?> validator,
       (ushort LowerBound, ushort UpperBound) bounds, string propertyName, Predicate<string>? skipMustBeNullPredicate) where TProduct : ProductEntity
        => MustBeNullOrBetweenFactory(validator, bounds, propertyName, skipMustBeNullPredicate);

    public static IRuleBuilder<TProduct, double?> MustBeNullOrBetween<TProduct>(this IRuleBuilder<TProduct, double?> validator,
       (ushort LowerBound, ushort UpperBound) bounds, string propertyName) where TProduct : ProductEntity
        => MustBeNullOrBetweenFactory(validator, bounds, propertyName);

    public static IRuleBuilder<TEntity, string?> MatchesAlphabet<TEntity>
        (this IRuleBuilder<TEntity, string?> validator, string propertyName, Predicate<string?>? matchesSkipPredicate = null) =>
        validator.CreateMatchesRule(Patterns.AlphabetPattern(), propertyName,
            MatchGroupMappers.CriteriaMap.GetValueOrDefault(GroupNames.Space), matchesSkipPredicate);

    public static IRuleBuilder<TEntity, string?> MatchesEmail<TEntity>
        (this IRuleBuilder<TEntity, string?> validator, string propertyName, Predicate<string?>? matchesSkipPredicate = null) where TEntity : EntityBase =>
        validator.CreateMatchesRule(Patterns.EmailPattern(), propertyName,
            MatchGroupMappers.CriteriaMap.GetValueOrDefault(GroupNames.Digital), matchesSkipPredicate);

    public static IRuleBuilder<TEntity, string?> MatchesAddress<TEntity>
        (this IRuleBuilder<TEntity, string?> validator, string propertyName) where TEntity : EntityBase =>
        validator.CreateMatchesRule(Patterns.AddressPattern(), propertyName);

    public static IRuleBuilder<TEntity, string?> MatchesPhone<TEntity>
        (this IRuleBuilder<TEntity, string?> validator, string propertyName, Predicate<string?>? matchesSkipPredicate = null) where TEntity : EntityBase =>
        validator.CreateMatchesRule(Patterns.PhonePattern(), propertyName, skipMatchesPredicate: matchesSkipPredicate);

    /// <summary><paramref name="keyName"/> must have ColumnAttribute defined on it! </summary>
    public static IRuleBuilder<TBase, Guid?> KeyMustExist<TBase, TEntity>(this IRuleBuilder<TBase, Guid?>
        validator, IRepository<TEntity> keyEntityRepository, string keyName)  where TEntity : EntityBase where TBase : EntityBase =>
        CreateKeyMustExist(validator, keyEntityRepository, keyName);

    /// <summary><paramref name="keyName"/> must have ColumnAttribute defined on it!</summary>
    public static IRuleBuilder<TValidatorBase, Guid> KeyMustExist<TValidatorBase, TEntity>(this IRuleBuilder<TValidatorBase, Guid>
    validator, IRepository<TEntity> keyEntityRepository, string keyName) where TValidatorBase : EntityBase where TEntity : EntityBase
        => CreateKeyMustExist(validator, keyEntityRepository, keyName);

    public static IRuleBuilder<TValidatorBase, Guid> ValidateRemoveReferences<TValidatorBase, TEntity, TEntity2, TEntityBase>(
        this IRuleBuilder<TValidatorBase, Guid> validator, IRepository<TEntity> foreignKeyOneRepository, IRepository<TEntity2> foreignKeyTwoRepository,
        Func<TEntityBase, Guid> foreignKeySelector, string fKeyName)
        where TValidatorBase : EntityBase where TEntity : TEntityBase where TEntity2 : TEntityBase where TEntityBase : EntityBase =>
            DeleteFactory(validator, foreignKeyOneRepository, foreignKeyTwoRepository, foreignKeySelector, fKeyName, typeof(TValidatorBase).Name);

    public static IRuleBuilder<ICollection<TValidatorBase>, Guid> ValidateRemoveReferencesForEach<TValidatorBase, TEntity, TEntity2, TEntityBase>(
        this IRuleBuilderInitialCollection<ICollection<TValidatorBase>, Guid> validator, IRepository<TEntity> foreignKeyOneRepository,
        Func<TEntityBase, Guid> foreignKeySelector, IRepository<TEntity2> foreignKeyTwoRepository, string fKeyName)
        where TValidatorBase : EntityBase where TEntity : TEntityBase where TEntity2 : TEntityBase where TEntityBase : EntityBase =>
            DeleteFactory(validator, foreignKeyOneRepository, foreignKeyTwoRepository, foreignKeySelector, fKeyName, typeof(TValidatorBase).Name);

    private static void TrackNewContragentKeys<TEntity, TEntity3>(ref (Guid? Id, Guid? EmployeeId) contragentKeys, TEntity entity, in Type contragentType)
        where TEntity3 : ContragentEntity
    {
        if (contragentType.IsAssignableTo(typeof(ContragentEntity)))
        {
            var updatedEntity = Unsafe.As<TEntity, ContragentEntity>(ref entity);
            contragentKeys = (updatedEntity.Id, updatedEntity.EmployeeId);
        }
        if (contragentType == typeof(SetEmployeeCommand<TEntity3>))
        {
            var updatedEntity = Unsafe.As<TEntity, SetEmployeeCommand<TEntity3>>(ref entity);
            contragentKeys = (updatedEntity.EntityId, updatedEntity.EmployeeId);
        }
    }
}

file static class MatchGroupMappers
{
    public static readonly ReadOnlyDictionary<GroupNames, Predicate<Match>> CriteriaMap = new(
        new Dictionary<GroupNames, Predicate<Match>>
        {
            [GroupNames.Digital] = (m) => m.Groups[NamesMap![GroupNames.Digital]].Success,
            [GroupNames.Space] = (m) => m.Groups[NamesMap![GroupNames.Space]].Captures.Count <= 1
        });

    private static readonly ReadOnlyDictionary<GroupNames, string> NamesMap = new(
        new Dictionary<GroupNames, string>
        {
            [GroupNames.Digital] = Enum.GetName(GroupNames.Digital)!,
            [GroupNames.Space] = Enum.GetName(GroupNames.Space)!
        });
}

file enum GroupNames : byte
{
    Space = 1,
    Digital
}