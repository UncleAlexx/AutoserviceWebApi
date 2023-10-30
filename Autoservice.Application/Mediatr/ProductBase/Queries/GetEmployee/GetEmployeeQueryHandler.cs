namespace Autoservice.Application.ProductBase.Queries.GetEmployee;

public sealed class GetEmployeeQueryHandler<TProduct> : IQueryHandler<GetEmployeeQuery<TProduct>, EmployeeEntity, EntityResult<EmployeeEntity>> where TProduct : ProductEntity
{
    private readonly IProductBaseRepository<TProduct> _productRepository;
    private readonly IContragentBaseRepository<ProviderEntity> _providerRepository;
    private readonly IEmployeeRepository _employeeRepository;

    private readonly EntityExistsValidator<TProduct> _productExistsValidator;

    public GetEmployeeQueryHandler(IProductBaseRepository<TProduct> repository, EntityExistsValidator<TProduct> productExistsValidator,
        IContragentBaseRepository<ProviderEntity> providerRepository, IEmployeeRepository employeeRepository) =>
        (_productRepository, _productExistsValidator, _providerRepository, _employeeRepository) = 
        (repository, productExistsValidator, providerRepository, employeeRepository);

    public async Task<EntityResult<EmployeeEntity>> Handle(GetEmployeeQuery<TProduct> request, CancellationToken token)
    {
        if ((await _productExistsValidator.ValidateAsync(request.ProductId, token)).IsValid)
        {
            EmployeeEntity employee = (await _productRepository.GetEmployeeAsync(request.ProductId, _providerRepository, _employeeRepository))!;
            return Result<EmployeeEntity>.Success<EntityResult<EmployeeEntity>>(employee);
        }
        return Result<EmployeeEntity>.Failed<EntityResult<EmployeeEntity>>(new EntityNotFoundException<TProduct>(request.ProductId, IdRaw));
    }
}
