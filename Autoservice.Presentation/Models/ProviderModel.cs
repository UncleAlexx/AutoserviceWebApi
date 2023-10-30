namespace Autoservice.Presentation.Models;

public static class ProviderModel
{
    public static IEndpointRouteBuilder AddProviderModel(this IEndpointRouteBuilder app)
    {
        (_, var providerQueriesGroup) = app.ConfigureOpenApi(MainGroupNames.ProviderMainGroup).AddCommonEndpoints<Provider>().
            AddContragentBaseEndpoints<Provider>();

        providerQueriesGroup.MapGet("/GetClients{id}", async Task<Results<Ok<ICollection<Client>>, NotFound<string>>>
            (ISender sender, Guid clientId, CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetClientsQuery(clientId)));

        return app;
    }
}
