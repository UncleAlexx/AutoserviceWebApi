namespace Autoservice.Application.ContragentBase.Queries.GetRevenue;

public sealed class GetRevenueQueryHandler<TContragent> : IQueryHandler<GetRevenueQuery<TContragent>, double, INumberBaseResult<double>> 
    where TContragent : ContragentEntity
{
    private readonly IContragentBaseRepository<TContragent> _contragentRepository;

    private readonly EntityExistsValidator<TContragent> _entityExistsValidator;
    public GetRevenueQueryHandler(IContragentBaseRepository<TContragent> contragentRepository, EntityExistsValidator<TContragent> contragentExistsValidator) =>
        (_contragentRepository, _entityExistsValidator) = (contragentRepository, contragentExistsValidator);

    public async Task<INumberBaseResult<double>> Handle(GetRevenueQuery<TContragent> request, CancellationToken token)
    {
        if ((await _entityExistsValidator.ValidateAsync(request.EntityId, token)).IsValid)
        {
            double result = await _contragentRepository.GetRevenueAsync(request.EntityId);
            return Result<double>.Success<INumberBaseResult<double>>(result);
        }
        return Result<double>.Failed<INumberBaseResult<double>>(new EntityNotFoundException<TContragent>(request.EntityId, IdRaw));
    }
}
