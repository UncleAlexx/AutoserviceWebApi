namespace Autoservice.Infrastructure.Repositories;

public sealed class ContragentBaseRepository<TEntity> : Repository<TEntity>, IContragentBaseRepository<TEntity>  where TEntity : ContragentBase
{
    private readonly IProductBaseRepository<Car> _carRepository;
    private readonly IProductBaseRepository<Part> _partRepository;

    public ContragentBaseRepository(AutoserviceContext autoserviceContext,
        IProductBaseRepository<Car> carRepository, IProductBaseRepository<Part> partRepository) : base(autoserviceContext) =>
        (_partRepository, _carRepository) = (partRepository, carRepository);

    public async ValueTask<Employee?> SetEmployeeAsync(Guid id, Guid employeeId, IRepository<Employee> employeeRepository) =>
        await id.SetForeignKeyPropertyAsync(employeeId, this, employeeRepository, nameof(ContragentBase.EmployeeId), employeeId);

    public async ValueTask<Employee?> GetEmployeeAsync(Guid contragentId, IRepository<Employee> employeeRepository) => 
        (await GetByIdAsync(contragentId))?.EmployeeId.GetEntity(employeeRepository.GetAll(), x => x.Id);

    public async ValueTask<ICollection<Part>> GetPartsAsync(Guid contragentId) => 
        await contragentId.GetEntitiesAsync(this, _partRepository.GetAll() , Select);

    public async ValueTask<ICollection<Car>> GetCarsAsync(Guid contragentId) => 
        await contragentId.GetEntitiesAsync(this, _carRepository.GetAll(), Select);

    public async ValueTask<double> GetRevenueAsync(Guid entityId)
    {
        if(await GetByIdAsync(entityId) is null)
            return 0;
        return _partRepository.GetAll().
            Where(x => Select(x) == entityId).Sum(x => x.Cost) + _carRepository.GetAll().
            Where(x => Select(x) == entityId).Sum(x => x.Cost);
    }

    private static Guid? Select<TProduct>(TProduct d) where TProduct : ProductBase => typeof(TEntity) == typeof(Provider) ? d.ProviderId : d.ClientId;

}
