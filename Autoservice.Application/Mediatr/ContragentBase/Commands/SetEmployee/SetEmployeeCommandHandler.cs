namespace Autoservice.Application.ContragentBase.Commands.SetEmployee;

public sealed class SetEmployeeCommandHandler<TContragent> : ICommandHandler<SetEmployeeCommand<TContragent>, EmployeeEntity, EntityResult<EmployeeEntity>>
    where TContragent : ContragentEntity
{
    private readonly IContragentBaseRepository<TContragent> _contragentRepository;
    private readonly IRepository<EmployeeEntity> _employeeRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly EntityExistsValidator<TContragent> _contragentExistsValidator;
    private readonly EntityExistsValidator<EmployeeEntity> _employeeExistsValidator;
    private readonly SetEmployeeCommandValidator<TContragent> _setEmployeeValidator;

    public SetEmployeeCommandHandler(IContragentBaseRepository<TContragent> contragentRepository, IRepository<EmployeeEntity> employeeRepository,
        EntityExistsValidator<TContragent> contragentExistsValidator, EntityExistsValidator<EmployeeEntity> employeeExistsValidator, 
        SetEmployeeCommandValidator<TContragent> setEmployeeValidator, IUnitOfWork unitOfWork) =>
        (_contragentRepository, _employeeRepository, _contragentExistsValidator, _employeeExistsValidator, _setEmployeeValidator, _unitOfWork) = 
        (contragentRepository, employeeRepository, contragentExistsValidator, employeeExistsValidator, setEmployeeValidator, unitOfWork);

    public async Task<EntityResult<EmployeeEntity>> Handle(SetEmployeeCommand<TContragent> request, CancellationToken token)
    {
        var setEmployeeValidationResult = await _setEmployeeValidator.ValidateAsync(request, token);
        var contragentValidationResult = await _contragentExistsValidator.ValidateAsync(request.EntityId, token);
        var employeeValidationResult = await _employeeExistsValidator.ValidateAsync(request.EmployeeId, token);
        if (new FluentValidation.Results.ValidationResult[] { setEmployeeValidationResult, contragentValidationResult, employeeValidationResult }.IsValid())
        {
            var result = (await _contragentRepository.SetEmployeeAsync(request.EntityId, request.EmployeeId, _employeeRepository))!;
            await _unitOfWork.SaveChangesAsync();
            return Result<EmployeeEntity>.Success<EntityResult<EmployeeEntity>>(result);
        }
        return Result<EmployeeEntity>.Failed<EntityResult<EmployeeEntity>>(new ValidationException(setEmployeeValidationResult.Errors.Concat(contragentValidationResult.Errors).Concat(employeeValidationResult.Errors)));
    }
}
