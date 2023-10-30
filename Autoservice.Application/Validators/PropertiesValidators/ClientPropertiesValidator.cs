namespace Autoservice.Application.Validators.PropertiesValidators;

internal sealed class ClientPropertiesValidator : EntityPropertiesValidator<ClientEntity>, IPropertiesValidator<ClientEntity>
{
    public ClientPropertiesValidator()
    {
        RuleFor(x => x.AdditionalEmail).MatchesEmail(AdditionalEmailRaw, IsNullOrEmpty);
        RuleFor(x => x.AdditionalPhone).MatchesPhone(AdditionalPhoneRaw, IsNullOrEmpty);
        RuleFor(x => x.Address).NotNull().NotEmpty().MatchesAddress(AddressRaw);
        RuleFor(x => x.Email).MatchesEmail(EmailRaw, IsNullOrEmpty);
        RuleFor(x => x.FullName).MinimumLength(4).MaximumLength(30).MatchesAlphabet(FullNameRaw) ;
        RuleFor(x => x.Phone).MatchesPhone(PhoneRaw);
        RuleFor(x => x.WorkNumber).MatchesPhone(WorkNumberRaw, IsNullOrEmpty);
    }
}