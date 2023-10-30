namespace Autoservice.Application.ContragentBase.Queries.GetRevenue;

public sealed record GetRevenueQuery<TContragent>(Guid EntityId) : IQuery<double, INumberBaseResult<double>> where TContragent : ContragentEntity;