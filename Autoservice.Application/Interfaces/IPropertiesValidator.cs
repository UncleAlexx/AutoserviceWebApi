namespace Autoservice.Application.Interfaces;

public interface IPropertiesValidator<TEntity> : IValidator<TEntity> where TEntity : EntityBase
{
}
