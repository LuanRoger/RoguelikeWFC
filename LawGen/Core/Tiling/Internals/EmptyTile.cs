namespace LawGen.Core.Tiling.Internals;

public record EmptyTile() : MapTile(0, 0, TileSocket.Empty)
{
    private static EmptyTile? _systemInstance;
    internal static EmptyTile SystemInstance => _systemInstance ??= new();
}