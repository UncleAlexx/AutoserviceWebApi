namespace Autoservice.Application.Employee.Queries.GetRevenue;

public sealed class GetRevenueQueryHandler : IQueryHandler<GetRevenueQuery, double, INumberBaseResult<double>>
{
    private readonly IEmployeeRepository _employeeRepository;

    private readonly EntityExistsValidator<EmployeeEntity> _employeeExistsValidator;

    public GetRevenueQueryHandler(IEmployeeRepository employeeRepository, EntityExistsValidator<EmployeeEntity> employeeExistsValidator) =>
        (_employeeRepository, _employeeExistsValidator) = (employeeRepository, employeeExistsValidator);

    public async Task<INumberBaseResult<double>> Handle(GetRevenueQuery request, CancellationToken token)
    {
        if ((await _employeeExistsValidator.ValidateAsync(request.EmployeeId, token)).IsValid)
        {
            var result = await _employeeRepository.GetRevenueAsync(request.EmployeeId);
            return Result<double>.Success<INumberBaseResult<double>>(result);
        }
        return Result<double>.Failed<INumberBaseResult<double>>(new EntityNotFoundException<EmployeeEntity>(request.EmployeeId, IdRaw));
    }
}
