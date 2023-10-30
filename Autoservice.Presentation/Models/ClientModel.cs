namespace Autoservice.Presentation.Models;

public static class ClientModel
{
    public static IEndpointRouteBuilder AddClientModel(this IEndpointRouteBuilder app)
    {
        app.ConfigureOpenApi(MainGroupNames.ClientMainGroup).AddCommonEndpoints<Client>().AddContragentBaseEndpoints<Client>();
        return app;
    }
}
