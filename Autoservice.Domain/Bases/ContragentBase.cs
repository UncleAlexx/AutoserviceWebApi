namespace Autoservice.Domain.Bases;

public abstract class ContragentBase : EntityBase
{
    [Column("EmployeeID")]
    public required Guid EmployeeId { get; set; }
}
