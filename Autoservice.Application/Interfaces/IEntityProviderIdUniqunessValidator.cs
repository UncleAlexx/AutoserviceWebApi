namespace Autoservice.Application.Interfaces;

public interface IEntityProviderIdUniquenessValidator<TEntity> : IValidator<TEntity> where TEntity : EntityBase
{
}
