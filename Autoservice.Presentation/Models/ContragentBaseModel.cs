using Autoservice.Domain.Bases;

namespace Autoservice.Presentation.Models;

internal static class ContragentBaseModel
{
    public static void AddContragentBaseCommands<TEntity>(this RouteGroupBuilder app) where TEntity : ContragentBase
    {
        app.MapPatch("/SetEmployee{id}/{employeeId}/", async Task<Results<NotFound<string>, Ok<Employee>, ValidationProblem>> (ISender sender, Guid id, 
            Guid employeeId, CancellationToken token) =>
            await EntityEndpointsFactory.CreatePatchRequest(sender, token, new Application.ContragentBase.Commands.SetEmployee.SetEmployeeCommand<TEntity>
                (id, employeeId)));
    }

    public static void AddContragentBaseQueries<TEntity>(this RouteGroupBuilder app) where TEntity : ContragentBase
    {
        app.MapGet("/GetCars{client}", async Task<Results<Ok<ICollection<Car>>, NotFound<string>>>
            (ISender sender, Guid clientId, CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetCarsQuery<TEntity>(clientId)));

        app.MapGet("/GetParts{id}", async Task<Results<Ok<ICollection<Part>>, NotFound<string>>>
            (ISender sender, Guid clientId, CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetPartsQuery<TEntity>(clientId)));

        app.MapGet("/GetRevenue{id}", async Task<Results<Ok<double>, NotFound<string>>>
           (ISender sender, Guid clientId, CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetRevenueQuery<TEntity>(clientId)));

        app.MapGet("/GetEmployee{id}", async Task<Results<Ok<Employee>, NotFound<string>>>
          (ISender sender, Guid clientId, CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new ContragentGetEmployee.GetEmployeeQuery<TEntity>(clientId)));
    }
}
