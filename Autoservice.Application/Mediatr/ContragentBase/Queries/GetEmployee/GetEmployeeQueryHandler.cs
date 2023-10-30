namespace Autoservice.Application.ContragentBase.Queries.GetEmployee;

public sealed class GetEmployeeQueryHandler<TContragent> : IQueryHandler<GetEmployeeQuery<TContragent>, EmployeeEntity, EntityResult<EmployeeEntity>> 
    where TContragent : ContragentEntity
{
    private readonly IContragentBaseRepository<TContragent> _contragentRepository;
    private readonly IRepository<EmployeeEntity> _employeeRepository;

    private readonly EntityExistsValidator<TContragent> _contragentExistsValidator;

    public GetEmployeeQueryHandler(IContragentBaseRepository<TContragent> contragentRepository, IRepository<EmployeeEntity> employeeRepository,
        EntityExistsValidator<TContragent> contragentExistsValidator) =>
        (_contragentRepository, _contragentExistsValidator, _employeeRepository) = (contragentRepository, contragentExistsValidator, employeeRepository);

    public async Task<EntityResult<EmployeeEntity>> Handle(GetEmployeeQuery<TContragent> request, CancellationToken token)
    {
        if ((await _contragentExistsValidator.ValidateAsync(request.EntityId, token)).IsValid)
        {
            EmployeeEntity employee = (await _contragentRepository.GetEmployeeAsync(request.EntityId, _employeeRepository))!;

            return Result<EmployeeEntity>.Success<EntityResult<EmployeeEntity>>(employee);
        }
        return Result<EmployeeEntity>.Failed<EntityResult<EmployeeEntity>>(new EntityNotFoundException<TContragent>(request.EntityId, IdRaw));
    }
}
