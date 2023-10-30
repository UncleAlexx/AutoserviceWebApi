namespace Autoservice.Domain.Results.Common;

public abstract class ResultBase<T> : IResult<T>
{
    public abstract T? Entity { get; init; }
    public abstract OperationResult Status { get; init; }
    public abstract string? ErrorMessage { get; init; }
    public abstract Exception? Error { get;init; }
}

public abstract class Result<T>
{
    public  static T2 Success<T2>(T Entity) where T2 : IResult<T>
    {
        return Unsafe.As<ResultWrapper<T>, T2>(ref Unsafe.AsRef(new ResultWrapper<T>(OperationResult.Success, Entity)));
    }
    public static T2 Failed<T2>(Exception exception) where T2 : IResult<T>
    {
        return Unsafe.As<ResultWrapper<T>, T2>(ref Unsafe.AsRef(new ResultWrapper<T>(OperationResult.Failed, exception)));
    }
}

file class ResultWrapper<T> : ResultBase<T>
{
    public override sealed required T? Entity { get; init; }
    public override sealed required OperationResult Status { get; init; }

    public override sealed string? ErrorMessage { get; init; }

    public override Exception? Error { get; init; }

    [SetsRequiredMembers]
    public ResultWrapper(OperationResult operationResult, T entity)
    {
        Status = operationResult;
        Entity = entity;
    }

    [SetsRequiredMembers]
    public ResultWrapper(OperationResult operationResult, Exception exception)
    {
        Status = operationResult;
        Error = exception;
        ErrorMessage = exception.Message;
    }
}