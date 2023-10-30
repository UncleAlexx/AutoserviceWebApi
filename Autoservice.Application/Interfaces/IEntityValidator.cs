namespace Autoservice.Application.Interfaces;

internal interface IEntityValidator<TEntity> : IValidator<TEntity> where TEntity : EntityBase
{
}
