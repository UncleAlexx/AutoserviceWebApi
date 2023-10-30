namespace Autoservice.Application.Interfaces;

public interface IDoesntExistValidator<TEntity> : IValidator<TEntity> where TEntity : EntityBase
{

}
