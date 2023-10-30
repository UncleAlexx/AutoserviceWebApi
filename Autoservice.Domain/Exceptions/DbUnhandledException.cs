namespace Autoservice.Domain.Exceptions;

public sealed class DbUnhandledException : Exception
{
    private readonly Exception _innerException;

    public DbUnhandledException(in Exception innerException) =>  _innerException = innerException;

    public override string Message => $"Unhandled exception by server happened or DB is shoutdown inner exception Message '{_innerException.Message}'";
}
