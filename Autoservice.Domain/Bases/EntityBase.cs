namespace Autoservice.Domain.Bases;

public abstract class EntityBase
{
    [Key, Column("ID")]
    public Guid Id { get; init; }
}
