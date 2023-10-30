namespace Autoservice.Infrastructure.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private readonly AutoserviceContext AutoserviceContext;

    protected Repository(AutoserviceContext autoserviceContext) => AutoserviceContext = autoserviceContext;

    public async ValueTask<TEntity?> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            return null;
        var entry = await AutoserviceContext.FindAsync<TEntity>(id);
        return entry is null || AutoserviceContext.Entry(entry!).State != EntityState.Unchanged ? null : entry;
    }

    public ICollection<TEntity> GetAll() => 
        GetDbSet().AsEnumerable().
        Where(x => AutoserviceContext.Entry(x).State == EntityState.Unchanged).
        ToList();
    

    public async ValueTask<TEntity?> AddAsync(TEntity entity)
    {
        if (entity == null || entity.Id != Guid.Empty)
            return null;
        var addition = await AutoserviceContext.AddAsync(entity);
        return addition.Entity;
    }

    public TEntity? Remove(TEntity entity)
    {
        if (entity is null || entity.Id == Guid.Empty)
            return null;
        var removal = AutoserviceContext.Remove(entity);
        return removal.State == EntityState.Deleted ? removal.Entity : null;
    }

    public async ValueTask<int> ClearAsync() => await GetDbSet().ExecuteDeleteAsync();

    public ICollection<TEntity> GetAllByIds(Guid[] ids)
    {
        ICollection<TEntity> entities = GetAll();
        if (ids is null or [] || entities.Any() is false || ids.All(x => x == Guid.Empty))
            return Array.Empty<TEntity>();
        return entities.Where(e => ids!.Any(i => i == e.Id)).ToList();
    }

    public TEntity? Update(TEntity entity)
    {
        if (entity is null || entity.Id == Guid.Empty || entity is null)
            return null;
        AutoserviceContext.Entry(entity).State = EntityState.Detached;
        AutoserviceContext.Update(entity);
        return entity;
    }

    private DbSet<TEntity> GetDbSet() =>
        typeof(TEntity) switch
        {
            var type when type == typeof(Part) => Unsafe.As<DbSet<Part>, DbSet<TEntity>>(ref Unsafe.AsRef(AutoserviceContext.Parts)),
            var type when type == typeof(Client) => Unsafe.As<DbSet<Client>, DbSet<TEntity>>(ref Unsafe.AsRef(AutoserviceContext.Clients)),
            var type when type == typeof(Provider) => Unsafe.As<DbSet<Provider>, DbSet<TEntity>>(ref Unsafe.AsRef(AutoserviceContext.Providers)),
            var type when type == typeof(Car) => Unsafe.As<DbSet<Car>, DbSet<TEntity>>(ref Unsafe.AsRef(AutoserviceContext.Cars)),
            var type when type == typeof(Employee) => Unsafe.As<DbSet<Employee>, DbSet<TEntity>>(ref Unsafe.AsRef(AutoserviceContext.Employees)),
            _ => Unsafe.As<TEntity[], DbSet<TEntity>>(ref Unsafe.AsRef(Array.Empty<TEntity>())),
        };

}