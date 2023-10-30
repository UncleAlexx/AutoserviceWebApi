namespace Autoservice.Infrastructure;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AutoserviceContext _context;

    public UnitOfWork(AutoserviceContext context) => _context = context;

    public async ValueTask<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
