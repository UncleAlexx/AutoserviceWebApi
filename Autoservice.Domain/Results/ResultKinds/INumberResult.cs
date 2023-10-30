namespace Autoservice.Domain.Results.ResultKinds;

public abstract class INumberBaseResult<TNumber> : ResultBase<TNumber> where TNumber : INumberBase<TNumber>
{
}
