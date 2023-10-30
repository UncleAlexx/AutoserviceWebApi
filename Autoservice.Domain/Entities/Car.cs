namespace Autoservice.Domain.Entities;

[Table(nameof(Car))]
public sealed class Car : ProductBase
{
    public required string Brand { get; set; }
    public required string Color { get; set; }
    public required double Cost { get; set; }
    public required string Type { get; set; }
    public required string Tires { get; set; }
    public required double Weight { get; set; }

    public double? Mileage { get; set; }
}
