namespace Autoservice.Application.Abtractions.Handlers;

public interface IQueryHandler<TQuery, TResponseType, TResponse> : 
    IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponseType, TResponse> where TResponse : IResult<TResponseType>
{
}