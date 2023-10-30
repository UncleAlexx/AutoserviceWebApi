namespace Autoservice.Presentation.Models;

internal static class ProductBaseModel
{
    public static void AddProductBaseCommands<TEntity>(this RouteGroupBuilder app) where TEntity : ProductBase
    {
        app.MapPatch("/SetClient/{entityId}/{clientId}", async Task<Results<NotFound<string>, Ok<Client>, ValidationProblem>> (ISender sender, Guid entityId, 
            Guid? clientId, CancellationToken token) =>
            await EntityEndpointsFactory.CreatePatchRequest(sender, token, new SetClientCommand<TEntity>(entityId, clientId)));
        app.MapPatch("/SetProvider/{entityId}/{providerId}", async Task<Results<NotFound<string>, Ok<Provider>, ValidationProblem>> (ISender sender, Guid entityId,
            Guid providerId, CancellationToken token) =>
            await EntityEndpointsFactory.CreatePatchRequest(sender, token, new SetProviderCommand<TEntity>(entityId, providerId)));
    }

    public static void AddProductBaseQueries<TEntity>(this RouteGroupBuilder app) where TEntity : ProductBase
    {
        app.MapGet("/GetClient{id}", async Task<Results<Ok<Client>, NotFound<string>>> (ISender sender, Guid carId, CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetClientQuery<TEntity>(carId)));

        app.MapGet("/GetEmployee{id}", async Task<Results<Ok<Employee>, NotFound<string>>> (ISender sender, Guid carId, CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetEmployeeQuery<TEntity>(carId)));

        app.MapGet("/GetProvider{id}", async Task<Results<Ok<Provider>, NotFound<string>>> (ISender sender, Guid carId, CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetProviderQuery<TEntity>(carId)));
    }
}
