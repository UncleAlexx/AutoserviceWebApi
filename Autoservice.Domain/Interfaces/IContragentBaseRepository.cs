namespace Autoservice.Domain.Interfaces;

public interface IContragentBaseRepository<TContragent> : IRepository<TContragent> where TContragent : ContragentBase
{
    ValueTask<Employee?> SetEmployeeAsync(Guid employeeId, Guid id, IRepository<Employee> repository);
    
    ValueTask<Employee?> GetEmployeeAsync(Guid Id, IRepository<Employee> repository);

    ValueTask<ICollection<Part>> GetPartsAsync(Guid id);

    ValueTask<ICollection<Car>> GetCarsAsync(Guid id);

    ValueTask<double> GetRevenueAsync(Guid id);
}
