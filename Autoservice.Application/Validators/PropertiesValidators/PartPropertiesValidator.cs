namespace Autoservice.Application.Validators.PropertiesValidators;

internal sealed class PartPropertiesValidator : EntityPropertiesValidator<PartEntity>, IPropertiesValidator<PartEntity>
{
    public PartPropertiesValidator()
    {
        RuleFor(x => x.Brand).NotNull().NotEmpty().MinimumLength(3).MaximumLength(30).MatchesAlphabet(BrandRaw);
        RuleFor(x => x.Cost).GreaterThan(0);
        RuleFor(x => x.Type).MustBeNullOrBetween((4, 30), TypeRaw, IsNullOrEmpty);
    }
}