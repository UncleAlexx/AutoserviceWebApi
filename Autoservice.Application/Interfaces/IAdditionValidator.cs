namespace Autoservice.Application.Interfaces;

public interface IAdditionValidator<TEntity> : IValidator<TEntity> where TEntity : EntityBase
{
}
