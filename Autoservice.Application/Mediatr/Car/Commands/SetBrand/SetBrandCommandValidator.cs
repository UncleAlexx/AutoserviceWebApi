using Autoservice.Application.Car.Commands.SetBrand;

namespace Autoservice.Application.Validators.RemoveValidators;

public sealed class SetBrandCommandValidator : AbstractValidator<SetBrandCommand>
{
    public SetBrandCommandValidator() => RuleFor(x => x.Brand).NotNull().NotEmpty().MinimumLength(4).MaximumLength(30).MatchesAlphabet(BrandRaw);
}
