namespace BarloPortfolio.Models;

public class ExperienceItem
{
    public required string Role { get; init; }
    public required string Organization { get; init; }
    public required string Period { get; init; }
    public required string Summary { get; init; }
    public required string[] Highlights { get; init; }
}
