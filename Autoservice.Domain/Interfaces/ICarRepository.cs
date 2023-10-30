namespace Autoservice.Domain.Interfaces;

public interface ICarRepository : IRepository<Car>
{
    ValueTask<Car?> SetBrand(Guid carId, string brand);
}
