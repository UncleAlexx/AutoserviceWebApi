namespace Autoservice.Application.Interfaces;

public interface IClearValidator<TEntity> : IValidator<ICollection<TEntity>>  where TEntity : EntityBase
{ 
}
