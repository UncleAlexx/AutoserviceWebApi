namespace Autoservice.Application.ContragentBase.Queries.GetParts;

public sealed record GetPartsQuery<TContragent>(Guid EntityId) : ICollectionQuery<PartEntity, EntityCollectionResult<PartEntity>> 
    where TContragent : ContragentEntity;
