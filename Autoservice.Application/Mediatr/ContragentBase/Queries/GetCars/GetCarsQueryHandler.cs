namespace Autoservice.Application.ContragentBase.Queries.GetCars;

public sealed class GetCarsQueryHandler<TContragent> : ICollectionQueryHandler<GetCarsQuery<TContragent>, CarEntity, EntityCollectionResult<CarEntity>> 
    where TContragent : ContragentEntity
{
    private readonly IContragentBaseRepository<TContragent> _contragentRepository;

    private readonly EntityExistsValidator<TContragent> _contragentExistsValidator;

    public GetCarsQueryHandler (IContragentBaseRepository<TContragent> contragentRepository, EntityExistsValidator<TContragent> contragentExistsValidator) =>
        (_contragentRepository, _contragentExistsValidator) = (contragentRepository, contragentExistsValidator);

    public async Task<EntityCollectionResult<CarEntity>> Handle(GetCarsQuery<TContragent> request, CancellationToken token)
    {
        if ((await _contragentExistsValidator.ValidateAsync(request.EntityId, token)).IsValid)
        {
            ICollection<CarEntity> cars = await _contragentRepository.GetCarsAsync(request.EntityId);
            return cars.Any() ? Result<ICollection<CarEntity>>.Success<EntityCollectionResult<CarEntity>>(cars) :
                Result<ICollection<CarEntity>>.Failed<EntityCollectionResult<CarEntity>>(new EntitiesNotFoundException<CarEntity, Guid>(request.EntityId, ClientIdRaw));
        }
        return Result<ICollection<CarEntity>>.Failed<EntityCollectionResult<CarEntity>>(new EntityNotFoundException<TContragent>(request.EntityId, IdRaw));
    }
}
