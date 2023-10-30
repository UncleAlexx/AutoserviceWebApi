namespace Autoservice.Application;

internal static class ValidationMessagesFactory
{
    public static string CreateProviderUniquenessErrorMessage<TContragent>(Guid employeeId, string contragentTypeName)
        where TContragent : ContragentEntity =>
        $"{contragentTypeName} {EmployeeIdRaw} unique constraint violation {EmployeeIdRaw} with value {employeeId} already set";

    public static string CreateClientIdErrorMessage(string entityName) => $"To set {entityName} {ClientIdRaw} {entityName} {ProviderRaw} and {ClientRaw}" +
        $" should have same {EmployeeIdRaw}";

    public static string CreateProviderIdErrorMessage(string entityName) => $"To set {entityName} {ProviderIdRaw} {ClientRaw} should be null";

    public static string CreateKeyMustExistMessage<TEntity>(this string keyName, string keyValue)
    {
        if (keyValue is null  || keyName is null)
            return UnknownErrorMessage;
 
        bool isPrimaryKey = typeof(TEntity).GetProperty(keyName)!.IsDefined(typeof(KeyAttribute));
        
        string foregnOrPrimaryAsRaw = isPrimaryKey ? PrimaryKeyRaw : ForeignRaw;
        return $"{foregnOrPrimaryAsRaw} '{keyName}' {MustExistMessage}. {foregnOrPrimaryAsRaw} " +
               $"'{keyName}' {WithValueMessage} '{keyValue}' {DoesntExistMessage}";
    }

    public static string CreateMatchesMessage(this string entityName, Regex pattern) => ErrorFilter(string.IsNullOrEmpty(entityName) || pattern is null,
        $"{entityName} should match pattern {pattern}");

    public static string CreateDeleteMessage<TOne, TTwo>(this string validationEntityName, string foreignKeyName,  
        Guid primaryKey, in bool referencesTOneFK) where TOne : EntityBase where TTwo : EntityBase
    {
        return ErrorFilter(string.IsNullOrEmpty(validationEntityName)  || string.IsNullOrEmpty(foreignKeyName) ,
        $"{ForeignRaw} conflict, cannot delete an entity called '{validationEntityName}', " +
        $"with {PrimaryKeyRaw} named '{IdRaw}', equal to '{primaryKey}', you must before delete a" +
        $" {(referencesTOneFK?typeof(TOne): typeof(TTwo)).Name} with {PrimaryKeyRaw} named '{IdRaw}', because it " +
        $"{ForeignRaw} called '{foreignKeyName}' references '{validationEntityName}' {PrimaryKeyRaw}" +
        $" named '{IdRaw}' and has value '{primaryKey}'");
    }

    public static string CreateMustHaveSameEmployeeIdMessage(this string entityName) => 
        ErrorFilter(string.IsNullOrEmpty(entityName), $"{entityName} should have {ClientRaw} and {ProviderRaw} serviced by the same {EmployeeRaw}");

    public static string CreateMustHaveEmployeeIdIntegrityMessage(this string entityName) =>
        ErrorFilter(string.IsNullOrEmpty(entityName), $"Some {CarRaw}s or {PartRaw}s have {ProviderRaw} and {ClientRaw} " +
            $"with not same {EmployeeIdRaw}, {entityName} should have {ClientRaw} and {ProviderRaw} serviced by the same {EmployeeRaw}");

    public static string CreateMustBeNullOrBetween(this string criteria, ref (ushort LowerBound, ushort UpperBound) bounds)
        => ErrorFilter(bounds.LowerBound >= bounds.UpperBound || string.IsNullOrEmpty(criteria),
            $"{criteria} should be >= {bounds.LowerBound} and <= {bounds.UpperBound}");

    private static string ErrorFilter(bool condition, string value) => condition is true ? UnknownErrorMessage : value;
}