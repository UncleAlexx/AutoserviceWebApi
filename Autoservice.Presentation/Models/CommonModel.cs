namespace Autoservice.Presentation.Models;

internal static class CommonModel
{
    public static void AddCommonCommands<TEntity>(this RouteGroupBuilder app) where TEntity : EntityBase
    {
        app.MapPost("/Add{entity}", async Task<Results<Created<TEntity>, ValidationProblem>>
            (TEntity entity, CancellationToken token, ISender sender) =>
            await EntityEndpointsFactory.CreateCreatedRequest(sender, token, new AddCommand<TEntity>(entity)));

        app.MapDelete("/Clear", async Task<Results<NotFound<string>, Accepted<int>, ValidationProblem>> (CancellationToken token, ISender sender) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new ClearCommand<TEntity>()));

        app.MapDelete("/Remove{id}", async Task<Results<NotFound<string>, Accepted<TEntity>, ValidationProblem>> (ISender sender, Guid entityId, 
            CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new RemoveCommand<TEntity>(entityId)));

        app.MapPatch("/Update{entity}", async Task<Results<NotFound<string>, Ok<TEntity>, ValidationProblem>> (ISender sender, TEntity entity, 
            CancellationToken token) =>
            await EntityEndpointsFactory.CreatePatchRequest(sender, token, new UpdateCommand<TEntity>(entity)));
    }

    public static void AddCommonQueries<TEntity>(this RouteGroupBuilder app) where TEntity: EntityBase
    {
        app.MapGet("/GetAllByIds{ids}", async Task<Results<Ok<ICollection<TEntity>>, NotFound<string>>> (ISender sender, Guid[] entityIds, CancellationToken token)
            => await EntityEndpointsFactory.CreateRequest(sender, token, new GetAllByIdsQuery<TEntity>(entityIds)));

        app.MapGet("/GetAll", async Task<Results<Ok<ICollection<TEntity>>, NotFound<string>>> (ISender sender, CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetAllQuery<TEntity>()));

        app.MapGet("/GetById{id}", async Task<Results<Ok<TEntity>, NotFound<string>>> (ISender sender, Guid entityId, CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetByIdQuery<TEntity>(entityId)));
    }
}
