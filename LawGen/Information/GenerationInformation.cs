namespace LawGen.Information;

public record GenerationInformation
{
    public int collapsedTiles { get; init; }
    public int conflictTiles { get; init; }
    public int leftTilesToCollapse { get; init; }
    public int totalTiles { get; init; }
    public bool allCollapsed { get; init; }
    public float progress => (float)(collapsedTiles - conflictTiles) / totalTiles;
}