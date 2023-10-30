namespace Autoservice.Application.Validators.AdditionValidators;

internal sealed class ContragentBaseAdditionValidator : AbstractValidator<ContragentEntity>, IAdditionValidator<ContragentEntity>
{
    public ContragentBaseAdditionValidator(IEmployeeRepository employeeRepository) =>
        RuleFor(x => x.EmployeeId).KeyMustExist(employeeRepository, EmployeeIdRaw);
} 