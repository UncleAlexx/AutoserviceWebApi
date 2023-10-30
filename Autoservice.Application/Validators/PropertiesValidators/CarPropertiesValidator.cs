namespace Autoservice.Application.Validators.PropertiesValidators;

internal sealed class CarPropertiesValidator : EntityPropertiesValidator<CarEntity>, IPropertiesValidator<CarEntity>
{
    public CarPropertiesValidator()
    {
        RuleFor(x => x.Brand).NotNull().NotEmpty().MinimumLength(4).MaximumLength(30).MatchesAlphabet(BrandRaw);
        RuleFor(x => x.Color).NotNull().NotEmpty().MinimumLength(4).MaximumLength(30).MatchesAlphabet(ColorRaw);
        RuleFor(x => x.Cost).GreaterThan(0);
        RuleFor(x => x.Mileage).MustBeNullOrBetween((0, 300), MileageRaw);
        RuleFor(x => x.Tires).NotNull().NotEmpty().MinimumLength(4).MaximumLength(30).MatchesAlphabet(TiresRaw);
        RuleFor(x => x.Type).NotNull().NotEmpty().MinimumLength(4).MaximumLength(30).MatchesAlphabet(TypeRaw);
        RuleFor(x => x.Weight).GreaterThan(0).LessThanOrEqualTo(200);
    }
}