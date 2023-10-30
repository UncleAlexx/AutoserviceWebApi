namespace Autoservice.Presentation;

internal static class EntityEndpointsFactory
{
    public async static Task<Results<Ok<TResultType>, NotFound<string>>> CreateRequest<TResultType, TRequestResult>(ISender sender, 
        CancellationToken token, IQuery<TResultType, TRequestResult> query)
        where TRequestResult : IResult<TResultType>
    {        
        var result = await sender.Send(query, token);
        return CreateRequestBase<Ok<TResultType>, NotFound<string>, TRequestResult, TResultType>
            (ref Unsafe.AsRef(TypedResults.Ok(result.Entity)), ref Unsafe.AsRef(TypedResults.NotFound(result.ErrorMessage)), result);
    }
    public async static Task<Results<Created<TResultType>, ValidationProblem>> CreateCreatedRequest<TResultType, TRequestResult>(ISender sender, 
        CancellationToken token, ICommand<TResultType, TRequestResult> query) 
        where TRequestResult : IResult<TResultType>
    {
        var result = await sender.Send(query, token);
        return CreateRequestBase<Created<TResultType>, ValidationProblem, TRequestResult, TResultType>
            (ref Unsafe.AsRef(TypedResults.Created("\\",result.Entity)), ref Unsafe.AsRef(CreateValidationProblem<TRequestResult,TResultType>(result)), result);
    }

    private static ValidationProblem CreateValidationProblem<TResultType, TRequestResult>(in TResultType result) where TResultType :
        IResult<TRequestResult> =>
        result.Status == OperationResult.Success ? TypedResults.ValidationProblem(_emptyErrors):
        TypedResults.ValidationProblem(Unsafe.As<Exception, FluentValidation.ValidationException>(ref Unsafe.AsRef(result.Error!)).Errors.ToDictionary(key =>
         key.PropertyName, value =>  value is null ? Array.Empty<string>() : new string[] { value.ErrorMessage }));

    public async static Task<Results<Ok<ICollection<TResultType>>, NotFound<string>>> CreateRequest<TResultType, TRequestResult>(ISender sender, 
        CancellationToken token, ICollectionQuery<TResultType, TRequestResult> query) 
        where TRequestResult : IResult<ICollection<TResultType>>
    {
        var result = await sender.Send(query, token);
        return result.Status == OperationResult.Success ? TypedResults.Ok(result.Entity) :
            TypedResults.NotFound(result.ErrorMessage);
    }

    public async static Task<Results<NotFound<string>, Accepted<TResultType>, ValidationProblem>> CreateRequest<TResultType, TRequestResult>(ISender sender,
        CancellationToken token, ICommand<TResultType, TRequestResult> query) where TRequestResult : IResult<TResultType>
    {
        var result = await sender.Send(query, token);
        if (result.Status == OperationResult.Failed && result!.Error!.GetType() != typeof(FluentValidation.ValidationException))
        {
            return TypedResults.NotFound(result.ErrorMessage);
        }
        return result.Status == OperationResult.Success ? TypedResults.Accepted("\\", result.Entity) :
            CreateValidationProblem<TRequestResult, TResultType>(result);
    }
    public async static Task<Results<NotFound<string>, Ok<TRequestType>, ValidationProblem>> CreatePatchRequest<TRequestType, TRequestResult>(ISender sender,
        CancellationToken token, ICommand<TRequestType, TRequestResult> query) 
        where TRequestResult : IResult<TRequestType>
    {
        var result = await sender.Send(query, token);
        if (result.Status == OperationResult.Failed && result!.Error!.GetType() != typeof(FluentValidation.ValidationException))
        {
            return TypedResults.NotFound(result.ErrorMessage);
        }
        return result.Status == OperationResult.Success ? TypedResults.Ok(result.Entity) : 
            CreateValidationProblem<TRequestResult, TRequestType>(result);
    }
    public static Results<HttpResultOne, HttpResultTwo>  CreateRequestBase<HttpResultOne, HttpResultTwo, TRequestType, TRequestResult>
        (ref HttpResultOne resultTwo, ref HttpResultTwo resultOne, TRequestType? result) where HttpResultOne : IResult where HttpResultTwo : IResult 
        where TRequestType : IResult<TRequestResult> =>
            result!.Status == OperationResult.Success ? resultTwo : resultOne;

    private static readonly Dictionary<string, string[]> _emptyErrors = new ();
}
