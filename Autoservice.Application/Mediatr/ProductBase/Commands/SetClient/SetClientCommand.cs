namespace Autoservice.Application.ProductBase.Commands.SetClient;

public sealed record SetClientCommand<TProduct>(Guid Id, Guid? ClientId) : ICommand<ClientEntity, EntityResult<ClientEntity>> where TProduct : ProductEntity;