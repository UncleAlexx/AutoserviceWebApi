using System.Net.Http.Headers;

namespace Autoservice.Application.Common.Commands.Clear;

public sealed class ClearCommandHandler<TEntity> : ICommandHandler<ClearCommand<TEntity>, int, INumberBaseResult<int>> where TEntity : EntityBase
{
    private readonly IRepository<TEntity> _repository;

    private readonly IClearValidator<TEntity> _clearValidator;

    public ClearCommandHandler(IRepository<TEntity> repository, IClearValidator<TEntity> clearValidator) =>
        (_repository, _clearValidator) = (repository, clearValidator);

    public async Task<INumberBaseResult<int>> Handle(ClearCommand<TEntity> request, CancellationToken token)
    {
        var validationResult = await _clearValidator.ValidateAsync(_repository.GetAll(), token);
        if (validationResult.IsValid)
        {
            int result;
            try
            {
                result = await _repository.ClearAsync();
            }
            catch (Exception ex)
            {
                return Result<int>.Failed<INumberBaseResult<int>>(new DbUnhandledException(ex));
            }
            return Result<int>.Success<INumberBaseResult<int>>(result);
        }
        return Result<int>.Failed<INumberBaseResult<int>>(new FluentValidation.ValidationException(validationResult.Errors));
    }
}
