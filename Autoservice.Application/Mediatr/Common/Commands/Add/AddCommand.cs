namespace Autoservice.Application.Common.Commands.Add;

public sealed record AddCommand<TEntity> (TEntity Entity) : ICommand<TEntity, EntityResult<TEntity>> where TEntity : EntityBase; 
