namespace Autoservice.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    ValueTask<TEntity?> GetByIdAsync(Guid id);
    ICollection<TEntity> GetAll();
    ValueTask<TEntity?> AddAsync(TEntity entity);
    TEntity? Remove(TEntity entity);
    ValueTask<int> ClearAsync();
    TEntity? Update(TEntity car);
    ICollection<TEntity> GetAllByIds(Guid[] ids);
}
