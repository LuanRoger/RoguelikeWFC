namespace RoguelikeWFC.WFC;

public struct TileSocket
{
    public static TileSocket Empty => new()
    {
        fitTop = Array.Empty<byte>(),
        fitRight = Array.Empty<byte>(),
        fitBottom = Array.Empty<byte>(),
        fitLeft = Array.Empty<byte>()
    };
    
    public required byte[] fitTop { get; init; }
    public required byte[] fitRight { get; init; }
    public required byte[] fitBottom { get; init; }
    public required byte[] fitLeft { get; init; }
}