namespace Autoservice.Presentation.Models;

public static class CarModel
{
    public static IEndpointRouteBuilder AddCarModel(this IEndpointRouteBuilder app)
    {
        (var carCommandGroup, var carQueriesGroup) = app.ConfigureOpenApi(MainGroupNames.CarMainGroup).AddCommonEndpoints<Car>().
            AddProductBaseEndpoints<Car>();

        carCommandGroup.MapPatch("/SetBrand/{carId}/{brand}", async Task<Results<NotFound<string>, Ok<Car>, ValidationProblem>> (ISender sender, Guid carId, 
            string brand, CancellationToken token) =>
            await EntityEndpointsFactory.CreatePatchRequest(sender, token, new SetBrandCommand(carId, brand)));
        return app;
    }
}
