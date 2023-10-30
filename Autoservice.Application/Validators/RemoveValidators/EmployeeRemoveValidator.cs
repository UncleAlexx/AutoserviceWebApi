namespace Autoservice.Application.Validators.RemoveValidators;

internal sealed class EmployeeRemoveValidator : AbstractValidator<EmployeeEntity>, IRemoveValidator<EmployeeEntity>
{
    public EmployeeRemoveValidator(IContragentBaseRepository<ClientEntity> clientRepository, IProviderRepository providerRepository) =>
        RuleFor(x => x.Id).ValidateRemoveReferences(clientRepository, providerRepository, ValidationConstants.Selectors.ContragentBaseSelector, EmployeeIdRaw);
}
