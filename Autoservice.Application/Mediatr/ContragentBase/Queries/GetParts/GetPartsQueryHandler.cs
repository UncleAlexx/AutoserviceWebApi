namespace Autoservice.Application.ContragentBase.Queries.GetParts;

public sealed class GetPartsQueryHandler<TContragent> : ICollectionQueryHandler<GetPartsQuery<TContragent>, PartEntity, EntityCollectionResult<PartEntity>>
    where TContragent :
    ContragentEntity
{
    private readonly IContragentBaseRepository<TContragent> _contragentRepository;

    private readonly EntityExistsValidator<TContragent> _contragentExistsValidator;

    public GetPartsQueryHandler(IContragentBaseRepository<TContragent> contragentRepository, EntityExistsValidator<TContragent> contragentExistsValidator) =>
        (_contragentRepository, _contragentExistsValidator) = (contragentRepository, contragentExistsValidator);

    public async Task<EntityCollectionResult<PartEntity>> Handle(GetPartsQuery<TContragent> request, CancellationToken token)
    {
        if ((await _contragentExistsValidator.ValidateAsync(request.EntityId, token)).IsValid)
        {
            ICollection<PartEntity> cars = await _contragentRepository.GetPartsAsync(request.EntityId);
            return cars.Any() ? Result<ICollection<PartEntity>>.Success<EntityCollectionResult<PartEntity>>(cars) :
                Result<ICollection<PartEntity>>.Failed<EntityCollectionResult<PartEntity>>
                    (new EntitiesNotFoundException<PartEntity, Guid>(request.EntityId, ProviderIdRaw));
        }
        return Result<ICollection<PartEntity>>.Failed<EntityCollectionResult<PartEntity>>(new EntityNotFoundException<TContragent>(request.EntityId, IdRaw));
    }
}
