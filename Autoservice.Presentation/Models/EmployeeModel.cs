namespace Autoservice.Presentation.Models;

public static class EmployeeModel
{
    public static IEndpointRouteBuilder AddEmployeeModel(this IEndpointRouteBuilder app)
    {
        (_, var employeeQueriesGroup) = app.ConfigureOpenApi(MainGroupNames.EmployeeMainGroup).AddCommonEndpoints<Employee>();
        employeeQueriesGroup.MapGet("/GetClients{id}", async Task<Results<Ok<ICollection<Client>>, NotFound<string>>> (ISender sender, Guid employeeId,
            CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetClientsQuery(employeeId)));

        employeeQueriesGroup.MapGet("/GetProvider{id}", async Task<Results<Ok<Provider>, NotFound<string>>> (ISender sender, Guid employeeId, 
            CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetProviderQuery(employeeId)));
        employeeQueriesGroup.MapGet("/GetRevenue{id}", async Task<Results<Ok<double>, NotFound<string>>> (ISender sender, Guid employeeId, CancellationToken token) =>
            await EntityEndpointsFactory.CreateRequest(sender, token, new GetRevenueQuery(employeeId)));
        return app;
    }
}
