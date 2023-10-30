namespace Autoservice.Application.Validators.ProviderIdUniqunessValidators;

internal sealed class ProviderProviderUniquenessValidator : AbstractValidator<ProviderEntity>,
    IEntityProviderIdUniquenessValidator<ProviderEntity>
{
    public ProviderProviderUniquenessValidator(IProviderRepository providerRepository, AutoserviceContext context) =>
        RuleFor(x => x).MustAsync(async (x, y) =>
        {
            var old = await providerRepository.GetByIdAsync(x.Id);
            if (old is null)
                return false;
            context.Entry(old).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            if (old.EmployeeId == x.EmployeeId)
                return true;
            var all = providerRepository.GetAll();
            return all.Any(z => z.EmployeeId == x.EmployeeId) is false;
        }).WithName(EmployeeIdRaw).WithMessage(provider => CreateProviderUniquenessErrorMessage<ProviderEntity>(provider.EmployeeId, ProviderRaw));
}
