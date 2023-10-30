namespace Autoservice.Presentation;

internal static class MainGroupNamesMapper
{
    internal static readonly ReadOnlyDictionary<MainGroupNames, string> MainGroupsNameMapper = new(
        new Dictionary<MainGroupNames, string>()
    {
        [MainGroupNames.CarMainGroup] = OpenApiConstants.CarModelMainGroup,
        [MainGroupNames.PartMainGroup] = OpenApiConstants.PartModelMainGroup,
        [MainGroupNames.EmployeeMainGroup] = OpenApiConstants.EmployeeModelMainGroup,
        [MainGroupNames.ProviderMainGroup] = OpenApiConstants.ProviderModelMainGroup,
        [MainGroupNames.ClientMainGroup] = OpenApiConstants.ClientModelMainGroup
    });
}
