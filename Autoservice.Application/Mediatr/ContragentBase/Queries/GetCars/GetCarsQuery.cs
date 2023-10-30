namespace Autoservice.Application.ContragentBase.Queries.GetCars;

public sealed record GetCarsQuery<TContragent>(Guid EntityId) : ICollectionQuery<CarEntity, EntityCollectionResult<CarEntity>> where TContragent : ContragentEntity;
