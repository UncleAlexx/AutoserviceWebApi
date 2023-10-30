namespace Autoservice.Application.ContragentBase.Commands.SetEmployee;

public sealed class SetEmployeeCommandValidator<TContragent> : AbstractValidator<SetEmployeeCommand<TContragent>> where TContragent : ContragentEntity
{
    public SetEmployeeCommandValidator(IRepository<CarEntity> carRepository, IRepository<PartEntity> partRepository,
        IRepository<ProviderEntity> providerRepository, IRepository<ClientEntity> clientRepository, IRepository<TContragent> contragentRepository)
    {
        string contragentTypeName = typeof(TContragent).Name;
        RuleFor(x => x).MustHaveEmployeeIdIntegrity(carRepository, providerRepository, partRepository, clientRepository, contragentTypeName,
            contragentRepository);
        RuleFor(x => x).MustAsync(async (x, y) =>
        {
            if (typeof(TContragent) == typeof(ClientEntity))
                return true;
            var provider = await providerRepository.GetByIdAsync(x.EntityId);
            if (provider is null)
                return false;
            if (provider.EmployeeId == x.EmployeeId)
                return true;
            return providerRepository.GetAll().Any(p => p.EmployeeId == x.EmployeeId) is false;
        }).WithName($"{ProviderRaw} uniqueness").WithMessage(setEmployee => 
            CreateProviderUniquenessErrorMessage<TContragent>(setEmployee.EmployeeId, contragentTypeName));
    }
}
