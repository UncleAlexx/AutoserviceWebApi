namespace Autoservice.Application.Common.Commands.Update;

public sealed record UpdateCommand<TEntity>(TEntity Entity) : ICommand<TEntity, EntityResult<TEntity>> where TEntity : EntityBase;
