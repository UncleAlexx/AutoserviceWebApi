namespace Autoservice.Domain.Entities;

[Table(nameof(Provider))]
public sealed class Provider : ContragentBase
{
    public required string Company { get; set; }
    public required string WorkPhone { get; set; }
    public required string Address { get; set; }
    public required string Phone { get; set; }
    public required string FullName { get; set; }

    public string? AdditionalPhone { get; set; }
    public string? Email { get; set; }
}
