namespace Autoservice.Application.Abtractions.Requests;

public interface ICollectionQuery<TRequest, TResponse> : IRequest<TResponse> where TResponse : IResult<ICollection<TRequest>>
{
}
