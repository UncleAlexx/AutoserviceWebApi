namespace Autoservice.Domain.Entities;

[Table(nameof(Part))]
public sealed class Part : ProductBase
{
    public required string Brand { get; set; }
    public required double Cost { get; set; }

    public string? Type { get; set; }
}
