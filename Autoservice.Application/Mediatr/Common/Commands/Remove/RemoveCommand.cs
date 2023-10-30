namespace Autoservice.Application.Common.Commands.Remove;

public sealed record RemoveCommand<TEntity>(Guid Id) : ICommand<TEntity, EntityResult<TEntity>> where TEntity : EntityBase;