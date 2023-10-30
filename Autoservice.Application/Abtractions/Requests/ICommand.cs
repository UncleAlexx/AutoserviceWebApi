namespace Autoservice.Application.Abtractions.Requests;

public interface ICommand<TRequest, TResponse> : IRequest<TResponse> where TResponse : IResult<TRequest>
{
}