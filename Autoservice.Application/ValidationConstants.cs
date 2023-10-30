namespace Autoservice.Application;

internal static class ValidationConstants
{
    internal static class Selectors
    {
        public static readonly Func<ContragentEntity, Guid> ContragentBaseSelector = c => c.EmployeeId;
        public static Guid ProductBaseSelector<TContragent>(ProductEntity product) where TContragent : ContragentEntity 
            => typeof(TContragent) == typeof(ProviderEntity) ? product.ProviderId : product.ClientId ?? Guid.Empty;
    }

    internal static class Constants
    {
        public const string IdRaw = nameof(EntityBase.Id);
        public const string BrandRaw = nameof(CarEntity.Brand);
        public const string ColorRaw = nameof(CarEntity.Color);
        public const string MileageRaw = nameof(CarEntity.Mileage);
        public const string AdditionalEmailRaw = nameof(ClientEntity.AdditionalEmail);
        public const string AdditionalPhoneRaw = nameof(ClientEntity.AdditionalPhone);
        public const string CompanyRaw = nameof(ProviderEntity.Company);
        public const string EmailRaw = nameof(ClientEntity.Email);
        public const string WorkNumberRaw = nameof(ClientEntity.WorkNumber);
        public const string FullNameRaw = nameof(EmployeeEntity.WorkPhone);
        public const string WorkPhoneRaw = nameof(ClientEntity.FullName);
        public const string PostRaw = nameof(EmployeeEntity.Post);
        public const string AddressRaw = nameof(ClientEntity.Address);
        public const string PhoneRaw = nameof(ClientEntity.Phone);
        public const string TiresRaw = nameof(CarEntity.Tires);
        public const string TypeRaw = nameof(CarEntity.Type);
        public const string ProviderIdRaw = nameof(ProductEntity.ProviderId);
        public const string ClientIdRaw = nameof(ProductEntity.ClientId);
        public const string EmployeeIdRaw = nameof(ContragentEntity.EmployeeId);
        public const string CarRaw = nameof(Car);
        public const string ProviderRaw = nameof(Provider);
        public const string PartRaw = nameof(Part);
        public const string EmployeeRaw = nameof(Employee);
        public const string ClientRaw = nameof(Client);
        public const string MustExistMessage = "must exist in the database";
        public const string DoesntExistMessage = "doesn't exists in the database";
        public const string ForeignRaw = "Foreign key";
        public const string PrimaryKeyRaw = "Primary key";
        public const string WithValueMessage = "with value";
        public const string UnknownErrorMessage = "An unkown error has occured while building error message";
        public const string ShouldMatchPatternMessage = "should match pattern";
    }
}
