namespace Autoservice.Application.ProductBase.Queries.GetClient;

public sealed record GetClientQuery<TProduct>(Guid ProductId) : IQuery<ClientEntity, EntityResult<ClientEntity>> where TProduct : ProductEntity;
