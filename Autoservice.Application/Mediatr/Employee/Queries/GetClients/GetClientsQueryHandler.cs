namespace Autoservice.Application.Employee.Queries.GetClients;

public sealed class GetClientsQueryHandler : ICollectionQueryHandler<GetClientsQuery, ClientEntity, EntityCollectionResult<ClientEntity>>
{
    private readonly IEmployeeRepository _employeeRepository;

    private readonly EntityExistsValidator<EmployeeEntity> _employeeExistsValidator;

    public GetClientsQueryHandler(IEmployeeRepository employeeRepository, EntityExistsValidator<EmployeeEntity> employeeExistsValidator) =>
        (_employeeExistsValidator, _employeeRepository) = (employeeExistsValidator, employeeRepository);

    public async Task<EntityCollectionResult<ClientEntity>> Handle(GetClientsQuery request, CancellationToken token)
    {
        if ((await _employeeExistsValidator.ValidateAsync(request.EmployeeId, token)).IsValid)
        {
            var clients = await _employeeRepository.GetClientsAsync(request.EmployeeId);
            return clients.Any() ? Result<ICollection<ClientEntity>>.Success<EntityCollectionResult<ClientEntity>>(clients) :
                Result<ICollection<ClientEntity>>.Failed<EntityCollectionResult<ClientEntity>>(new EntitiesNotFoundException<ClientEntity, Guid>
                    (request.EmployeeId, EmployeeIdRaw));
        }
        return Result<ICollection<ClientEntity>>.Failed<EntityCollectionResult<ClientEntity>>(
            new EntityNotFoundException<EmployeeEntity>(request.EmployeeId, IdRaw));
    }
}
