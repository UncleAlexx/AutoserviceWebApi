namespace Autoservice.Domain.Entities;

[Table(nameof(Client))]
public sealed class Client : ContragentBase
{
    public required string FullName { get; set; }
    public required string Address { get; set; }
    public required string Phone { get; set; }

    public string? Email { get; set; }
    public string? WorkNumber { get; set; }
    public string? AdditionalPhone { get; set; }
    public string? AdditionalEmail { get; set; }
}