namespace Autoservice.Domain.Interfaces;

public interface IResult<T>
{
    Exception? Error { get; init; }
    T? Entity { get; init; }
    OperationResult Status { get; init; }
    string? ErrorMessage {  get; init; }
}
