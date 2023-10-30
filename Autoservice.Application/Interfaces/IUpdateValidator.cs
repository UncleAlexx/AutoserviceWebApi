namespace Autoservice.Application.Interfaces;

public interface IUpdateValidator<TEntity> : IValidator<TEntity> where TEntity : EntityBase
{
}
