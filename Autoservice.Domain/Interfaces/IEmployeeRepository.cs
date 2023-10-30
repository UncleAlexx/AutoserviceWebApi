namespace Autoservice.Domain.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Provider? GetProvider(Guid employeeId);
    ValueTask<ICollection<Client>> GetClientsAsync(Guid employeeId);
    ValueTask<double> GetRevenueAsync(Guid employeeId);
}
