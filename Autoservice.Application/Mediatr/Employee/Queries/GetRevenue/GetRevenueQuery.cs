namespace Autoservice.Application.Employee.Queries.GetRevenue;

public sealed record GetRevenueQuery(Guid EmployeeId) : IQuery<double, INumberBaseResult<double>>;
