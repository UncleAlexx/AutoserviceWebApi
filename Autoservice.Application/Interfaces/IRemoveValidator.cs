namespace Autoservice.Application.Interfaces;

public interface IRemoveValidator<TEntity> : IValidator<TEntity> where TEntity : EntityBase
{
}
