namespace Autoservice.Application.Extensions;

internal static partial class IRuleBuilderExtensions
{
    private static IRuleBuilder<TValidatorBase, Guid> DeleteFactory<TValidatorBase, TEntity, TEntity2, TEntityBase>(this IRuleBuilder<TValidatorBase, Guid>
    validator, IRepository<TEntity> foreignKeyOneRepository, IRepository<TEntity2> foreignKeyTwoRepository, Func<TEntityBase, Guid> foreignKeySelector,
        string foreignKeyName, string validationEntityName)
        where TEntity : TEntityBase where TEntity2 : TEntityBase where TEntityBase : EntityBase
    {
        Unsafe.SkipInit(out bool referencesFKOne);
        Guid primaryKey = Guid.Empty;
        bool ReferencesFK<T>(in IRepository<T> repository, Guid primaryKey) where T : TEntityBase =>
            repository.GetAll().Any(x => foreignKeySelector(x) == primaryKey);
        return validator.Must(pK =>
        {
            if (pK == Guid.Empty)
                return false;
            var (hasreferenceToFKOne, hasreferenceToFKTwo) = (ReferencesFK(foreignKeyOneRepository, pK), ReferencesFK(foreignKeyTwoRepository, pK));

            TrackingHelpers.TrackVariable(ref primaryKey, pK);
            TrackingHelpers.TrackVariable(ref referencesFKOne, hasreferenceToFKOne);
            return (hasreferenceToFKOne || hasreferenceToFKTwo) is false;
        }).WithName(foreignKeyName).WithMessage(validationEntityName.CreateDeleteMessage<TEntity, TEntity2>(foreignKeyName, primaryKey, referencesFKOne));
    }

    private static IRuleBuilder<TEntity, string?> CreateMatchesRule<TEntity>(this IRuleBuilder<TEntity, string?> validator,
        Regex pattern, string propertyName, Predicate<Match>? matchesGroupPredicate = null, Predicate<string?>? skipMatchesPredicate = null)
        => validator.Must((prop) =>
        {
            if (prop is null || skipMatchesPredicate?.Invoke(prop) is not null and true)
                return true;
            bool shouldMatchGroup = matchesGroupPredicate is not null;
            if (shouldMatchGroup)
            {
                Match match = pattern.Match(prop);
                return matchesGroupPredicate!.Invoke(match);
            }
            return pattern.IsMatch(prop);
        }).WithMessage(propertyName.CreateMatchesMessage(pattern));

    /// <summary><paramref name="keyName"/> must have ColumnAttribute defined on it!</summary>
    private static IRuleBuilder<ValidatorBase, TKey> CreateKeyMustExist<ValidatorBase, KeyEntityType, TKey>(this IRuleBuilder<ValidatorBase, TKey>
    validator, IRepository<KeyEntityType> keyRepository, string keyName)
    where ValidatorBase : EntityBase where KeyEntityType : EntityBase
    {
        Unsafe.SkipInit(out TKey keyValue);
        return validator.MustAsync(async (key, ctx) =>
        {
            if (typeof(ValidatorBase).GetProperty(keyName)?.IsDefined(typeof(ColumnAttribute)) is false || key is not null && 
                key.GetType().IsAssignableTo(typeof(Guid?)) is false)
                return false;
            if (key is null)
                return true;
            var exists = (await keyRepository.GetByIdAsync(default(TKey) is null ?
                Unsafe.As<TKey, Guid?>(ref key)!.Value : Unsafe.As<TKey, Guid>(ref key))) is not null;
            TrackingHelpers.TrackVariable(ref keyValue, key);
            return exists;
        }).WithName(keyName).WithMessage(x => keyName.CreateKeyMustExistMessage<ValidatorBase>($"{keyValue}")); 
    }

    public static IRuleBuilder<TProduct, TProduct> MustHaveSameEmployeeId<TProduct>(
        this IRuleBuilder<TProduct, TProduct> validator, IRepository<ProviderEntity> providerRepository,
            IRepository<ClientEntity> clientRepository) where TProduct : ProductEntity
        => validator.MustAsync(async (product, ctx) =>
        {
            if (product.ClientId is null)
                return true;
            var provider = await providerRepository.GetByIdAsync(product.ProviderId);
            var client = await clientRepository.GetByIdAsync(product.ClientId.Value);
            return client?.EmployeeId == provider?.EmployeeId;
        }).WithName(ClientIdRaw).
            WithMessage(typeof(TProduct).Name.CreateMustHaveSameEmployeeIdMessage());

    private static IRuleBuilder<TValidatorBase, TValidatorBase> MustHaveEmployeeIdIntegrityFactory<TValidatorBase, TContragent>(
        this IRuleBuilder<TValidatorBase, TValidatorBase> validator, IRepository<CarEntity> carRepository, IRepository<ProviderEntity> providerRepository,
        IRepository<PartEntity> partRepository, IRepository<ClientEntity> clientRepository, string contragentName, IRepository<TContragent> contragentRepository,
        AutoserviceContext? context = null)
        where TContragent : ContragentEntity
    {
        Type contragentType = typeof(TValidatorBase);
        Unsafe.SkipInit(out (Guid? Id, Guid? EmployeeId) contragentKeys);

        return validator.MustAsync(async (validationBase, ctx) =>
        {
            TrackNewContragentKeys<TValidatorBase, TContragent>(ref contragentKeys, validationBase, contragentType);
            TContragent? oldEntity = await contragentRepository.GetByIdAsync(contragentKeys.Id ?? Guid.Empty);
            if (oldEntity is null || contragentKeys.EmployeeId is null)
                return false;
            if (oldEntity.EmployeeId == contragentKeys.EmployeeId)
                return true;
            TrackingHelpers.TrackVariable(ref contragentKeys, contragentKeys with { EmployeeId = oldEntity.EmployeeId });
            var provider = providerRepository.GetAll().FirstOrDefault(z => z.EmployeeId == contragentKeys.EmployeeId);
            var clients = clientRepository.GetAll().Where(z => z.EmployeeId == contragentKeys.EmployeeId).ToList();
            if (contragentType.IsAssignableTo(typeof(ContragentEntity)))
            {
                context!.Entry(oldEntity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                if (contragentType == typeof(Client))
                {
                    context!.Entry(clients.Single(x => x.Id == contragentKeys.Id)!).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                }
                else
                {
                    context!.Entry(provider!).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                }
            }
            var cars = carRepository.GetAll().Where(c => ValidationConstants.Selectors.ProductBaseSelector<TContragent>(c) == contragentKeys.Id);
            var parts = partRepository.GetAll().Where(z => ValidationConstants.Selectors.ProductBaseSelector<TContragent>(z) == contragentKeys.Id);
            bool exists = (cars.Any(car => car.ClientId.HasValue && clients.Any(c => c.Id == car.ClientId)) ||
            parts.Any(part => part.ClientId.HasValue && clients.Any(x => x.Id == part.ClientId) && provider!.Id == part.ProviderId));
            return !exists;
        }).WithName(typeof(TContragent).Name).WithMessage(contragentName.CreateMustHaveEmployeeIdIntegrityMessage());
    }

    private static IRuleBuilder<TEntity, TProperty> MustBeNullOrBetweenFactory<TEntity, TProperty>(this IRuleBuilder<TEntity, TProperty> validator,
        (ushort LowerBound, ushort UpperBound) bounds, string propertyName, Predicate<string>? skipMustBeNullPredicate = null) where TEntity : EntityBase
    {
        Type tPropertyType = typeof(TProperty);
        Console.WriteLine(tPropertyType);
        double? ValidationaCriteriaSelector(TProperty entity) =>
            tPropertyType == typeof(string) ? Unsafe.As<TProperty, string>(ref entity).Length :
            tPropertyType == typeof(double?) ? Unsafe.As<TProperty, double?>(ref entity) : null;

        return validator.Must(prop =>
        {
            if (prop is null || (skipMustBeNullPredicate is not null && skipMustBeNullPredicate(Unsafe.As<TProperty, string>(ref prop))))
                return true;
            TrackingHelpers.TrackVariable(ref propertyName, tPropertyType == typeof(string) ? $"{propertyName} length" : propertyName);
            double? validationCriteria = ValidationaCriteriaSelector(prop);
            return validationCriteria.HasValue && validationCriteria >= bounds.LowerBound && validationCriteria <= bounds.UpperBound;
        }).WithName(propertyName).WithMessage(propertyName.CreateMustBeNullOrBetween(ref bounds));
    }
}