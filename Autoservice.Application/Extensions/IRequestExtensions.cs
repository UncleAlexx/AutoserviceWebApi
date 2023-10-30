namespace Autoservice.Application.Extensions;

internal static partial class IRequestExtensions
{
    public static bool IsValid(this IEnumerable<FluentValidation.Results.ValidationResult> validators) =>
        validators.All(v => v.IsValid);
}

