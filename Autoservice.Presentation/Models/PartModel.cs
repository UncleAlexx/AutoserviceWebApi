namespace Autoservice.Presentation.Models;

public static class PartModel
{
    public static IEndpointRouteBuilder AddPartModel(this  IEndpointRouteBuilder app)
    {
        app.ConfigureOpenApi(MainGroupNames.PartMainGroup).AddProductBaseEndpoints<Part>().AddCommonEndpoints<Part>();
        return app;
    }
}
