namespace BarloPortfolio.Models;

public class SkillGroup
{
    public required string Title { get; init; }
    public required string Icon { get; init; }
    public required string[] Skills { get; init; }
}

public class ProficiencyItem
{
    public required string Name { get; init; }
    public required int Percent { get; init; }
}
