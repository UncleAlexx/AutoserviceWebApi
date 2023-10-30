namespace Autoservice.Application.ProductBase.Commands.SetProvider;

public sealed record SetProviderCommand<TProduct>(Guid Id, Guid ProviderId) : ICommand<ProviderEntity, EntityResult<ProviderEntity>> where TProduct : ProductEntity;