namespace Autoservice.Domain.Bases;

public abstract class ProductBase : EntityBase
{
    [Column("ProviderID")]
    public required Guid ProviderId { get; set; }

    
    [Column("ClientID")]
    public Guid? ClientId { get; set; }
}
