namespace Autoservice.Application.ProductBase.Queries.GetProvider;

public sealed record GetProviderQuery<TProduct>(Guid ProductId) : IQuery<ProviderEntity, EntityResult<ProviderEntity>> where TProduct : ProductEntity;