namespace Autoservice.Presentation;

internal static class OpenApiBuilder
{
    public static (RouteGroupBuilder commandsBuilder, RouteGroupBuilder queriesBuilder) ConfigureOpenApi(
        this IEndpointRouteBuilder endpointRouteBuilder, MainGroupNames mainGroupName)
    {
        var rawMainGroupName = MainGroupNamesMapper.MainGroupsNameMapper[mainGroupName];
        var mainGroup = endpointRouteBuilder.MapGroup(rawMainGroupName);
        return (mainGroup.MapGroup(OpenApiConstants.CommandsSubGroup).WithTags($"{rawMainGroupName} {OpenApiConstants.CommandsSubGroup}"),
            mainGroup.MapGroup(OpenApiConstants.QueriesSubGroup).WithTags($"{rawMainGroupName} {OpenApiConstants.QueriesSubGroup}"));
    }

    public static (RouteGroupBuilder commandsBuilder, RouteGroupBuilder queriesBuilder) AddCommonEndpoints<T>(this
        (RouteGroupBuilder commandsBuilder, RouteGroupBuilder queriesBuilder) builders) where T : EntityBase
    {
        builders.commandsBuilder.AddCommonCommands<T>();
        builders.queriesBuilder.AddCommonQueries<T>();
        return builders;
    }

    public static (RouteGroupBuilder commandsBuilder, RouteGroupBuilder queriesBuilder) AddProductBaseEndpoints<T>(this 
        (RouteGroupBuilder commandsBuilder, RouteGroupBuilder queriesBuilder) builders) where T : ProductBase
    {
        builders.commandsBuilder.AddProductBaseCommands<T>();
        builders.queriesBuilder.AddProductBaseQueries<T>();
        return builders;
    }

    public static (RouteGroupBuilder commandsBuilder, RouteGroupBuilder queriesBuilder) AddContragentBaseEndpoints<T>(this 
        (RouteGroupBuilder commandsBuilder, RouteGroupBuilder queriesBuilder) builders) where T : ContragentBase
    {
        builders.commandsBuilder.AddContragentBaseCommands<T>();
        builders.queriesBuilder.AddContragentBaseQueries<T>();
        return builders;
    }
}
