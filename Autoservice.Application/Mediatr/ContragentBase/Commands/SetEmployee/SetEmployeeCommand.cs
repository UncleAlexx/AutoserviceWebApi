namespace Autoservice.Application.ContragentBase.Commands.SetEmployee;

public sealed record SetEmployeeCommand<TContragent>(Guid EntityId, Guid EmployeeId) : ICommand<EmployeeEntity, EntityResult<EmployeeEntity>>
    where TContragent : ContragentEntity;