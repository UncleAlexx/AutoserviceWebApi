namespace Autoservice.Application.Car.Commands.SetBrand;

public sealed class SetBrandCommandHandler : ICommandHandler<SetBrandCommand, CarEntity, EntityResult<CarEntity>>
{
    private readonly ICarRepository _repository;

    private readonly EntityExistsValidator<CarEntity> _carExistsValidator;
    private readonly SetBrandCommandValidator _setBrandValidator;

    private readonly IUnitOfWork _unitOfWork; 

    public SetBrandCommandHandler(ICarRepository repository, EntityExistsValidator<CarEntity> carExistsValidator, 
        SetBrandCommandValidator setBrandValidator, IUnitOfWork unitOfWork) => 
        (_repository, _carExistsValidator, _setBrandValidator, _unitOfWork) = (repository, carExistsValidator, setBrandValidator, unitOfWork);   

    public async Task<EntityResult<CarEntity>> Handle(SetBrandCommand request, CancellationToken token)
    {
        var validationResults = await Task.WhenAll(_carExistsValidator.ValidateAsync(request.CarId, token),  
            _setBrandValidator.ValidateAsync(request, token));
        if (validationResults.IsValid())
        {
            var modified = (await _repository.SetBrand(request.CarId, request.Brand))!;
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<CarEntity>.Failed<EntityResult<CarEntity>>(new DbUnhandledException(ex));
            }
            return Result<CarEntity>.Success<EntityResult<CarEntity>>(modified);
        }
        return Result<CarEntity>.Failed<EntityResult<CarEntity>>(new ValidationException(validationResults.SelectMany(x => x.Errors)));
    }
}
