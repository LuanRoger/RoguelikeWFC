namespace RoguelikeWFC.WFC;

public struct TileSocket
{
    public static TileSocket Empty => new()
    {
        fitTop = Array.Empty<int>(),
        fitRight = Array.Empty<int>(),
        fitBottom = Array.Empty<int>(),
        fitLeft = Array.Empty<int>()
    };
    
    public required int[] fitTop { get; init; }
    public required int[] fitRight { get; init; }
    public required int[] fitBottom { get; init; }
    public required int[] fitLeft { get; init; }
}