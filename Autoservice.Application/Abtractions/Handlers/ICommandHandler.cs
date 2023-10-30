namespace Autoservice.Application.Abtractions.Handlers;

public interface ICommandHandler<TCommand, TResponseType, TReponse> : 
    IRequestHandler<TCommand, TReponse> where TCommand : ICommand<TResponseType, TReponse> where TReponse : IResult<TResponseType>
{
}