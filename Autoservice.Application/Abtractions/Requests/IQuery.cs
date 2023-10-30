namespace Autoservice.Application.Abtractions.Requests;

public interface IQuery<TRequest, TResponse> : IRequest<TResponse> where TResponse : IResult<TRequest>
{
}