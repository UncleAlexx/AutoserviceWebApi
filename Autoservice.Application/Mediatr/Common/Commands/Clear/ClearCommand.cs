namespace Autoservice.Application.Common.Commands.Clear;

public sealed record ClearCommand<TEntity> : ICommand<int, INumberBaseResult<int>> where TEntity : EntityBase;