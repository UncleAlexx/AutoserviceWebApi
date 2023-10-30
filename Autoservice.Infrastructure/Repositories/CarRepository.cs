namespace Autoservice.Infrastructure.Repositories;

public sealed class CarRepository : Repository<Car>, ICarRepository
{
    public CarRepository(AutoserviceContext autoserviceContext) : base(autoserviceContext) { }

    public async ValueTask<Car?> SetBrand(Guid carId, string brand) => await carId.SetPropertyAsync(nameof(Car.Brand), this, brand, false);
}
