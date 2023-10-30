namespace Autoservice.Application.Validators.UpdateValidators;

internal sealed class EmployeeUpdateValidator : AbstractValidator<EmployeeEntity>, IUpdateValidator<EmployeeEntity> 
{
    public EmployeeUpdateValidator(IRepository<CarEntity> carRepository, IRepository<PartEntity> partRepository,
        IRepository<ProviderEntity> providerRepository, IRepository<ClientEntity> clientRepository)
    {
        RuleFor(x => x).Must(x =>
        {
            var provider = providerRepository.GetAll().First(z => z.EmployeeId == x.Id);
            var clients = clientRepository.GetAll().Where(z => z.EmployeeId == x.Id);
            var cars = carRepository.GetAll().Where(z => provider.Id == z.ProviderId || clients.Any(y => y.Id == z.ClientId));
            var parts = partRepository.GetAll().Where(z => provider.Id == z.ProviderId || clients.Any(y => y.Id == z.ClientId));
            return cars.All(z => z.ClientId.HasValue is false || provider.Id == z.ProviderId && clients.Any(y => y.Id == z.ClientId)) &&
                parts.All(z => z.ClientId.HasValue is false || provider.Id == z.ProviderId && clients.Any(y => y.Id == z.ClientId));
        }).WithName(EmployeeRaw).WithMessage(EmployeeRaw.CreateMustHaveEmployeeIdIntegrityMessage());
    }
}
