namespace Autoservice.Application.Validators.ClearValidators;

internal sealed class EmployeeClearValidator : AbstractValidator<ICollection<EmployeeEntity>>, IClearValidator<EmployeeEntity>
{
    public EmployeeClearValidator(IContragentBaseRepository<ClientEntity> clientRepository, IContragentBaseRepository<ProviderEntity> proiderRepository) =>
        RuleForEach(x => x.Select(x => x.Id).ToList()).ValidateRemoveReferencesForEach(clientRepository, ValidationConstants.Selectors.ContragentBaseSelector, 
            proiderRepository, EmployeeIdRaw);
}
