namespace Autoservice.Application.Validators.UpdateValidators;

internal sealed class ContragentBaseUpdateValidator<TContragent> : AbstractValidator<TContragent>, IUpdateValidator<TContragent> 
    where TContragent : ContragentEntity
{
    public ContragentBaseUpdateValidator(IRepository<CarEntity> carRepository, IRepository<PartEntity> partRepository,
        IRepository<ProviderEntity> providerRepository, IRepository<ClientEntity> clientRepository, IRepository<TContragent> contragentRepository, 
        AutoserviceContext context) =>
        RuleFor(x => x).MustHaveEmployeeIdIntegrity(carRepository, providerRepository, partRepository, clientRepository, contragentRepository, 
            typeof(TContragent).Name, context);
}