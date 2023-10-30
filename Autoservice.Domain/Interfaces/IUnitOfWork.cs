namespace Autoservice.Domain.Interfaces;

public interface IUnitOfWork
{
    ValueTask <int> SaveChangesAsync();
}
