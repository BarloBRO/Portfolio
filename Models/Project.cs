namespace BarloPortfolio.Models;

public class Project
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Icon { get; init; }
    public required string[] Tags { get; init; }
    public bool Featured { get; init; }
}
