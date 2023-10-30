namespace Autoservice.Domain.Entities;

[Table(nameof(Employee))]
public sealed class Employee : EntityBase
{
    public required string Post { get; set; }
    public required string Address { get; set; }
    public required string Phone { get; set; }
    public required double Salary { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string WorkPhone { get; set; }

    public string? AdditionalPhone { get; set; }
    public string? AdditionalEmail { get; set; }
}
