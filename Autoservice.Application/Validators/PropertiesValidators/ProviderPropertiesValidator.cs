namespace Autoservice.Application.Validators.PropertiesValidators;

internal sealed class ProviderPropertiesValidator : EntityPropertiesValidator<ProviderEntity>, IPropertiesValidator<ProviderEntity>
{
    public ProviderPropertiesValidator()
    {
        RuleFor(x => x.Company).NotNull().NotEmpty().MinimumLength(10).MaximumLength(40).MatchesAlphabet(CompanyRaw);
        RuleFor(x => x.AdditionalPhone).MatchesPhone(AdditionalPhoneRaw, IsNullOrEmpty);
        RuleFor(x => x.Address).NotNull().NotEmpty().MatchesAddress(AddressRaw);
        RuleFor(x => x.Email).MatchesEmail(EmailRaw, IsNullOrEmpty);
        RuleFor(x => x.FullName).NotNull().NotEmpty().MinimumLength(10).MaximumLength(40).MatchesAlphabet(FullNameRaw);
        RuleFor(x => x.Phone).NotNull().NotEmpty().MatchesPhone(PhoneRaw);
        RuleFor(x => x.WorkPhone).NotNull().NotEmpty().MatchesPhone(WorkPhoneRaw);
    }
}