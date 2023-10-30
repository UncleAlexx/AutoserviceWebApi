namespace Autoservice.Application.Provider.Queries.GetClients;

public sealed class GetClientsQueryHandler : ICollectionQueryHandler<GetClientsQuery, ClientEntity,
    EntityCollectionResult<ClientEntity>>
{
    private readonly IProviderRepository _providerRepository;

    private readonly EntityExistsValidator<ProviderEntity> _providerExistsValidator;

    public GetClientsQueryHandler(IProviderRepository providerRepository, EntityExistsValidator<ProviderEntity> providerExistsValidator) =>
        (_providerRepository, _providerExistsValidator) = (providerRepository, providerExistsValidator);

    public async Task<EntityCollectionResult<ClientEntity>> Handle(GetClientsQuery request, CancellationToken token)
    {
        var providerExistsValidationResult = await _providerExistsValidator.ValidateAsync(request.ProviderId, token);
        if (providerExistsValidationResult.IsValid)
        {
            ICollection<ClientEntity> clients = _providerRepository.GetClients(request.ProviderId);
            return clients.Any() ? Result<ICollection<ClientEntity>>.Success<EntityCollectionResult<ClientEntity>>(clients) :
                Result<ICollection<ClientEntity>>.Failed<EntityCollectionResult<ClientEntity>>
                    (new EntitiesNotFoundException<ClientEntity, Guid>(request.ProviderId, ProviderIdRaw));
        }
        return Result<ICollection<ClientEntity>>.Failed<EntityCollectionResult<ClientEntity>>(new FluentValidation.ValidationException(providerExistsValidationResult.Errors));
    }
}
