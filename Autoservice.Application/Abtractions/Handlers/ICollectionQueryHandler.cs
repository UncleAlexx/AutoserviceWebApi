namespace Autoservice.Application.Abtractions.Handlers;

public interface ICollectionQueryHandler<TQuery, TResponseCollectionType, TResponse> : 
    IRequestHandler<TQuery, TResponse> where TQuery : ICollectionQuery<TResponseCollectionType, TResponse> where TResponse : 
    IResult<ICollection<TResponseCollectionType>>
{
}