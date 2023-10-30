namespace Autoservice.Application.Car.Commands.SetBrand;

public sealed record SetBrandCommand(Guid CarId, string Brand) : ICommand<CarEntity, EntityResult<CarEntity>>;
