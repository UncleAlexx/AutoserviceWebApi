namespace Autoservice.Application.Employee.Queries.GetProvider;

public sealed class GetProviderQueryHandler : IQueryHandler<GetProviderQuery, ProviderEntity, EntityResult<ProviderEntity>>
{
    private readonly IEmployeeRepository _employeeValidator;

    private readonly EntityExistsValidator<EmployeeEntity> _employeeExistsValidator;

    public GetProviderQueryHandler(IEmployeeRepository employeeRepository, EntityExistsValidator<EmployeeEntity> employeeExistsValidator) =>
        (_employeeExistsValidator, _employeeValidator) = (employeeExistsValidator, employeeRepository);

    public async Task<EntityResult<ProviderEntity>> Handle(GetProviderQuery request, CancellationToken token)
    {
        if ((await _employeeExistsValidator.ValidateAsync(request.EmployeeId, token)).IsValid)
        {
            var result = _employeeValidator.GetProvider(request.EmployeeId);
            return result is null ? Result<ProviderEntity>.Failed<EntityResult<ProviderEntity>>(new ForeignKeyNotFoundException<EmployeeEntity, ProviderEntity>
                (ProviderIdRaw, request.EmployeeId)) : Result<ProviderEntity>.Success<EntityResult<ProviderEntity>>(result);
        }
        return Result<ProviderEntity>.Failed<EntityResult<ProviderEntity>>(new EntityNotFoundException<EmployeeEntity>(request.EmployeeId, IdRaw));
    }
}
