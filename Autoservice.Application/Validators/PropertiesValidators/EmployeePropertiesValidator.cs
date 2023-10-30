namespace Autoservice.Application.Validators.PropertiesValidators;

internal sealed class EmployeePropertiesValidator : EntityPropertiesValidator<EmployeeEntity>, IPropertiesValidator<EmployeeEntity>
{
    public EmployeePropertiesValidator()
    {
        RuleFor(x => x.AdditionalEmail).MatchesEmail(AdditionalEmailRaw, IsNullOrEmpty);
        RuleFor(x => x.AdditionalPhone).MatchesPhone(AdditionalPhoneRaw, IsNullOrEmpty);
        RuleFor(x => x.Address).NotNull().NotEmpty().MatchesAddress(AddressRaw);
        RuleFor(x => x.Email).NotNull().NotEmpty().MatchesEmail(EmailRaw);
        RuleFor(x => x.FullName).NotNull().NotEmpty().MatchesAlphabet(FullNameRaw);
        RuleFor(x => x.Phone).NotNull().NotEmpty().MatchesPhone(PhoneRaw);
        RuleFor(x => x.Post).NotNull().MinimumLength(4).MaximumLength(30).MatchesAlphabet(PostRaw);
        RuleFor(x => x.Salary).GreaterThan(0);
        RuleFor(x => x.WorkPhone).NotNull().NotEmpty().MatchesPhone(WorkPhoneRaw);
    }
}
